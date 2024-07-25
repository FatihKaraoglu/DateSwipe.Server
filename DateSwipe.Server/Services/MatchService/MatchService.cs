using DateSwipe.Server.Data.DataContext;
using DateSwipe.Server.Hub;
using DateSwipe.Server.Services.AuthService;
using DateSwipe.Shared;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace DateSwipe.Server.Services.MatchService
{
    public class MatchService : IMatchService
    {
        private readonly DatingDbContext _context;
        private readonly IHubContext<DateIdeasHub> _hubContext;
        private readonly IAuthService _authService;

        public MatchService(DatingDbContext context, IHubContext<DateIdeasHub> hubContext, IAuthService authService)
        {
            _context = context;
            _hubContext = hubContext;
            _authService = authService;
        }

        public async Task SendMatchNoification(int coupleId, string message, int dateId)
        {
            Console.WriteLine($"Sending match notification to couple ID: {coupleId} with message: {message} and date ID: {dateId}");
            await _hubContext.Clients.Group(coupleId.ToString()).SendAsync("ReceiveMatchNotification", message, dateId);
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
                // Send notification via SignalR
                //await _hubContext.Clients.Group(coupleId.Value.ToString()).SendAsync("ReceiveMatchNotification", matchMessage);
                if(coupleId != null)
                {
                    await SendMatchNoification(coupleId.Value, matchMessage, dateId);
                }
                
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