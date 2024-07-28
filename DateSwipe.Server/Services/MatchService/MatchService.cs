using DateSwipe.Server.Data.DataContext;
using DateSwipe.Server.Hub;
using DateSwipe.Server.PushNotificationService;
using DateSwipe.Server.Services.AuthService;
using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DateSwipe.Server.Services.MatchService
{
    public class MatchService : IMatchService
    {
        private readonly DatingDbContext _context;
        private readonly IHubContext<DateIdeasHub> _hubContext;
        private readonly IAuthService _authService;
        private readonly IPushNotificationService _pushNotificationService;

        public MatchService(DatingDbContext context, IHubContext<DateIdeasHub> hubContext, IAuthService authService, IPushNotificationService pushNotificationService)
        {
            _context = context;
            _hubContext = hubContext;
            _authService = authService;
            _pushNotificationService = pushNotificationService;
        }

        public async Task SendMatchNotification(int coupleId, string message, int dateId)
        {
            Console.WriteLine($"Sending match notification to couple ID: {coupleId} with message: {message} and date ID: {dateId}");
            await _hubContext.Clients.Group(coupleId.ToString()).SendAsync("ReceiveMatchNotification", message, dateId);

            // Send push notifications to both users in the couple
            var coupleUsers = await _context.Users.Where(u => u.CoupleId == coupleId).ToListAsync();
            foreach (var user in coupleUsers)
            {
                var subscriptionsResponse = await _pushNotificationService.GetSubscriptionsAsync(user.Id);
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
                            title = "New Match!",
                            body = message
                        };

                        await _pushNotificationService.SendNotificationAsync(subscription, JsonSerializer.Serialize(payload));
                    }
                }
            }
        }

        public async Task<ServiceResponse<string>> Swipe(int dateId, bool liked)
        {
            var response = new ServiceResponse<string>();

            // Get the current user ID
            var userId = _authService.GetUserId();
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
                return response;
            }

            var coupleId = user.CoupleId;

            if (!coupleId.HasValue)
            {
                response.Success = false;
                response.Message = "User is not part of a couple.";
                return response;
            }

            // Save the swipe action
            var swipe = new UserSwipe
            {
                UserId = userId,
                DateIdeaId = dateId,
                Liked = liked,
                CoupleId = coupleId.Value
            };
            _context.UserSwipes.Add(swipe);
            await _context.SaveChangesAsync();

            // Check for a match
            var match = await _context.UserSwipes
                .Where(s => s.DateIdeaId == dateId && s.CoupleId == coupleId.Value && s.Liked)
                .GroupBy(s => s.DateIdeaId)
                .Where(g => g.Count() > 1) // At least two users liked this idea
                .Select(g => g.Key)
                .FirstOrDefaultAsync();

            if (match != 0)
            {
                var dateIdea = await _context.DateIdeas.FindAsync(dateId);
                var matchMessage = $"It's a match! Date Idea: {dateIdea.Title}, Date ID: {dateId}, Time: {DateTime.Now}";

                response.Data = "Match";
                response.Success = true;
                response.Message = "It's a match!";

                // Send notification via SignalR and Push Notification
                await SendMatchNotification(coupleId.Value, matchMessage, dateId);
            }
            else
            {
                response.Data = "No match";
                response.Success = true;
                response.Message = "Swipe registered.";
            }

            return response;
        }
    }
}
