﻿using DateSwipe.Server.Data.DataContext;
using DateSwipe.Server.Hub;
using DateSwipe.Server.PushNotificationService;
using DateSwipe.Server.Services;
using DateSwipe.Server.Services.AuthService;
using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DateSwipe.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly DatingDbContext _context;
        private readonly IAuthService _authService;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IPushNotificationService _pushNotificationService;

        public ChatMessagesController(DatingDbContext context, IAuthService authService, IHubContext<ChatHub> hubContext, IPushNotificationService pushNotificationService)
        {
            _context = context;
            _authService = authService;
            _hubContext = hubContext;
            _pushNotificationService = pushNotificationService;
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessage([FromBody] string messageContent)
        {
            var userId = _authService.GetUserId();
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            if (!user.CoupleId.HasValue)
            {
                return BadRequest("User is not part of a couple");
            }

            var chatMessage = new ChatMessage
            {
                UserId = userId,
                CoupleId = user.CoupleId.Value,
                Message = messageContent,
                Timestamp = DateTime.UtcNow,
                UserName = user.Name
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            // Notify the other user in the couple
            var coupleId = user.CoupleId.Value;
            await _hubContext.Clients.Group(coupleId.ToString()).SendAsync("ReceiveMessage", chatMessage);

            // Send push notification to the partner
            var partner = await _context.Users.FirstOrDefaultAsync(u => u.CoupleId == coupleId && u.Id != userId);
            if (partner != null)
            {
                var subscriptionsResponse = await _pushNotificationService.GetSubscriptionsAsync(partner.Id);

                if (subscriptionsResponse.Success && subscriptionsResponse.Data != null)
                {
                    foreach (var subscriptionDto in subscriptionsResponse.Data)
                    {
                        var subscription = new Lib.Net.Http.WebPush.PushSubscription
                        {
                            Endpoint = subscriptionDto.Endpoint,
                            Keys = subscriptionDto.Keys
                        };

                        var payload = new
                        {
                            title = "New Message",
                            body = $"New message from {user.Name}: {messageContent}"
                        };

                        await _pushNotificationService.SendNotificationAsync(subscription, JsonSerializer.Serialize(payload));
                    }
                }
            }

            return Ok(chatMessage);
        }

        [HttpGet]
        public async Task<ServiceResponse<List<ChatMessageDTO>>> GetMessages()
        {
            var userId = _authService.GetUserId();
            var user = await _context.Users.FindAsync(userId);
            List<ChatMessageDTO> chatMessages = new List<ChatMessageDTO>();

            if (user == null)
            {
                return new ServiceResponse<List<ChatMessageDTO>>
                {
                    Data = new List<ChatMessageDTO>(),
                    Message = "Couldnt retrieve User-Object!",
                    Success = false
                };
            }

            var messages = await _context.ChatMessages
                .Where(m => m.CoupleId == user.CoupleId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            // Find the partner user in the couple
            var partner = await _context.Users
                .Where(u => u.CoupleId == user.CoupleId && u.Id != user.Id)
                .FirstOrDefaultAsync();

            if (partner == null)
            {
                return new ServiceResponse<List<ChatMessageDTO>>
                {
                    Data = new List<ChatMessageDTO>(),
                    Message = "Couldnt retrieve Partner-Object!",
                    Success = false
                };
            }
                var likedDateIdeas = await _context.DateIdeas
                    .Where(di => _context.UserSwipes.Any(us => us.UserId == user.Id && us.Liked && us.DateIdeaId == di.Id)
                                && _context.UserSwipes.Any(us => us.UserId == partner.Id && us.Liked && us.DateIdeaId == di.Id))
                    .Select(di => new
                    {
                        di.Id,
                        di.Title,
                        di.Description,
                        di.DateIdeaCategories,
                        di.ImageUrl,
                        MatchTimestamp = _context.UserSwipes
                            .Where(us => us.DateIdeaId == di.Id && us.Liked &&
                                         (us.UserId == user.Id || us.UserId == partner.Id))
                            .Max(us => us.TimeStamp) 
                    })
                    .ToListAsync();

            var dateProposals = await _context.DateProposals.
                Include(dp => dp.DateIdea).
                ThenInclude(dc => dc.DateIdeaCategories).
                Where(di => di.CoupleId == user.CoupleId).ToListAsync();


            foreach(var message in messages)
            {
                ChatMessageDTO chatMessageDTO = new ChatMessageDTO
                {
                    CoupleId = message.CoupleId,
                    DateIdea = null,
                    DateProposal = null,
                    Type = MessageType.User,
                    TimeStamp = message.Timestamp,
                    Message = message.Message,
                    UserId = message.UserId,
                    UserName = message.UserName
                    
                   
                };
                chatMessages.Add(chatMessageDTO);
            }

            foreach(var dateProposal in dateProposals)
            {
                ChatMessageDTO chatMessageDTO = new ChatMessageDTO
                {
                    CoupleId = dateProposal.CoupleId,
                    DateProposal = new DateProposalDTO
                    {
                        Accept = dateProposal.Accept,
                        DateIdea = new DateIdeaDTO
                        {
                            Description = dateProposal.DateIdea.Description,
                            Id = dateProposal.DateIdeaId,
                            ImageUrl = dateProposal.DateIdea.ImageUrl,
                            Title = dateProposal.DateIdea.Title,
                            
                        },
                        DateProposalIssuer = dateProposal.DateProposalIssuer,
                        Canceled = dateProposal.Canceled,
                        CoupleId = dateProposal.CoupleId,
                        FromTime = dateProposal.FromTime,
                        TimeStamp = dateProposal.TimeStamp,
                        Id = dateProposal.Id,
                        ToTime = dateProposal.FromTime,
                        
                    },
                    Type = MessageType.DateProposal,
                    Message = null,
                    TimeStamp = dateProposal.TimeStamp,
                    UserId = dateProposal.DateProposalIssuer,
                    UserName = user.Name

                };

                chatMessageDTO.DateProposal.DateIdea.Categories = dateProposal.DateIdea.DateIdeaCategories
                .Select(dic => {
                    if (dic.Category == null)
                    {
                        // Log warning or error here
                        Console.WriteLine($"Warning: Null category found for DateIdeaId: {dateProposal.DateIdea.Id}");
                    }

                    return new CategoryDto
                    {
                        Id = dic.Category?.Id ?? 0,
                        Name = dic.Category?.Name ?? "Unknown"
                    };
                })
                .ToList();

                chatMessages.Add(chatMessageDTO);
            }

            foreach (var dateIdea in likedDateIdeas)
            {
                // Initialize a new ChatMessageDTO
                ChatMessageDTO chatMessageDTO = new ChatMessageDTO
                {
                    CoupleId = (int)user.CoupleId,
                    DateIdea = new DateIdeaDTO
                    {
                        Id = dateIdea.Id,
                        Description = dateIdea.Description,
                        ImageUrl = dateIdea.ImageUrl,
                        Title = dateIdea.Title
                        
                    },
                    TimeStamp = dateIdea.MatchTimestamp,
                    UserName = user.Name,
                    UserId = user.Id,
                    Type = MessageType.Match
                   
                };

                chatMessageDTO.DateIdea.Categories = dateIdea.DateIdeaCategories
                .Select(dic => {
                    if (dic.Category == null)
                    {
                        // Log warning or error here
                        Console.WriteLine($"Warning: Null category found for DateIdeaId: {dateIdea.Id}");
                    }

                    return new CategoryDto
                    {
                        Id = dic.Category?.Id ?? 0,
                        Name = dic.Category?.Name ?? "Unknown"
                    };
                })
                .ToList();


                chatMessages.Add(chatMessageDTO);
            }



            var orderedChatMessageDTOs = new ServiceResponse<List<ChatMessageDTO>>()
            {
                Data = chatMessages.OrderBy(d => d.TimeStamp).ToList(),
                Message = "",
                Success = true

            };

            return orderedChatMessageDTOs;
        }
    }
}
