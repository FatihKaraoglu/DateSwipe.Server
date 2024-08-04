using DateSwipe.Server.Services.AuthService;
using DateSwipe.Server.Data.DataContext;
using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DateSwipe.Server.Services.ProfileService;
using DateSwipe.Server.Hub;
using Microsoft.AspNetCore.SignalR;
using DateSwipe.Server.PushNotificationService;

namespace DateSwipe.Server.Services.DateProposalService
{
    public class DateProposalService : IDateProposalService
    {
        private readonly DatingDbContext _context;
        private readonly IAuthService _authService;
        private readonly IProfileService _profileService;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IPushNotificationService _pushNotificationService;


        public DateProposalService(DatingDbContext context, IAuthService authService, IProfileService profileService, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _authService = authService;
            _profileService = profileService;
            _hubContext = hubContext;
        }

        public async Task<ServiceResponse<DateProposal>> CreateDateProposalAsync(int DateIdeaId, DateTime From, DateTime To)
        {
            var response = new ServiceResponse<DateProposal>();
            var userId = _authService.GetUserId();
            var coupleId = (await _context.Users.Where(x => x.Id == _authService.GetUserId()).FirstOrDefaultAsync()).CoupleId;
            var dateIdea = await _context.DateIdeas.Where(x => x.Id == DateIdeaId).FirstOrDefaultAsync();


            DateProposal dateProposal = new DateProposal()
            {
                DateIdeaId = DateIdeaId,
                DateProposalIssuer = userId,
                CoupleId = (int)coupleId,
                Accept = null,
                FromTime = From,
                ToTime = To,
                DateIdea = dateIdea,
                TimeStamp = DateTime.Now,
            };

            try
            {
                _context.DateProposals.Add(dateProposal);
                await _context.SaveChangesAsync();

                response.Data = dateProposal;
                response.Success = true;
                await SendDateProposal(dateProposal);
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while creating the date proposal: {ex.Message}";
            }

            return response;
        }

        private async Task SendDateProposal(DateProposal dateProposal)
        {
            var chatMessageDTO = new ChatMessageDTO()
            {
                CoupleId = dateProposal.CoupleId,
                Type = MessageType.DateProposal,
                Message = null,
                TimeStamp = dateProposal.TimeStamp,
                UserId = dateProposal.DateProposalIssuer,
                UserName = (await _profileService.GetProfile()).Data.UserName,
                DateProposal = new DateProposalDTO
                {
                    DateIdea = new DateIdeaDTO
                    {
                        Description = dateProposal.DateIdea.Description,
                        Id = dateProposal.DateIdea.Id,
                        ImageUrl = dateProposal.DateIdea.ImageUrl,
                        Title = dateProposal.DateIdea.Title,
                    },
                    DateProposalIssuer = dateProposal.DateProposalIssuer,
                    Accept = dateProposal.Accept,
                    Canceled = dateProposal.Canceled,
                    CoupleId = dateProposal.CoupleId,

                },
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
            _hubContext.Clients.Group(dateProposal.CoupleId.ToString()).SendAsync("ReceiveDateProposal", chatMessageDTO);
        }

        public async Task<ServiceResponse<DateProposal>> GetDateProposalAsync(int proposalId)
        {
            var response = new ServiceResponse<DateProposal>();

            try
            {
                var proposal = await _context.DateProposals.FindAsync(proposalId);

                if (proposal == null)
                {
                    response.Success = false;
                    response.Message = "Date proposal not found.";
                }
                else
                {
                    response.Data = proposal;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while retrieving the date proposal: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<List<DateProposal>>> GetDateProposalsForCoupleAsync(int coupleId)
        {
            var response = new ServiceResponse<List<DateProposal>>();

            try
            {
                var proposals = await _context.DateProposals
                    .Where(dp => dp.CoupleId == coupleId)
                    .ToListAsync();

                response.Data = proposals;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while retrieving the date proposals: {ex.Message}";
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> AcceptDateProposalAsync(int proposalId)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var proposal = await _context.DateProposals
                    .Include(dp => dp.DateIdea) // Include related DateIdea
                    .FirstOrDefaultAsync(dp => dp.Id == proposalId);

                if (proposal == null)
                {
                    response.Success = false;
                    response.Message = "Date proposal not found.";
                    return response;
                }

                proposal.Accept = true;

                    var plannedDate = new PlannedDate
                    {
                        CoupleId = proposal.CoupleId,
                        DateIdeaId = proposal.DateIdeaId,
                        DateIdea = proposal.DateIdea, 
                        From = proposal.FromTime,
                        To = proposal.ToTime,
                        DateProposalId = proposal.Id
                    };

                    _context.PlannedDates.Add(plannedDate);

                _context.DateProposals.Update(proposal);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Success = true;
                response.Message = "Date proposal accepted.";

                var cancelledProposals = await _context.DateProposals.Where(x => x.CoupleId == proposal.CoupleId && x.Id != proposal.Id).ToListAsync();
                foreach(var cancelProposal in cancelledProposals)
                {
                    cancelProposal.Canceled = false;
                }
                _context.DateProposals.UpdateRange(cancelledProposals);
                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> RejectDateProposalAsync(int proposalId)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var proposal = await _context.DateProposals.FindAsync(proposalId);

                if (proposal == null)
                {
                    response.Success = false;
                    response.Message = "Date proposal not found.";
                }
                else if (proposal.DateProposalIssuer == _authService.GetUserId())
                {
                    response.Success = false;
                    response.Message = "Proposal issuer cannot reject their own proposal.";
                }
                else
                {
                    _context.DateProposals.Remove(proposal);
                    await _context.SaveChangesAsync();

                    response.Data = true;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred while rejecting the date proposal: {ex.Message}";
            }

            return response;
        }
    }
}
