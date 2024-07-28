namespace DateSwipe.Server.Hub
{
    using DateSwipe.Server.Data.DataContext;
    using DateSwipe.Server.PushNotificationService;
    using DateSwipe.Server.Services;
    using DateSwipe.Server.Services.AuthService;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ChatHub : Hub
    {
        private readonly IAuthService _authService;
        private readonly DatingDbContext _dbContext;
        private readonly IPushNotificationService _pushNotificationService;

        public ChatHub(IAuthService authService, DatingDbContext dbContext, IPushNotificationService pushNotificationService)
        {
            _authService = authService;
            _dbContext = dbContext;
            _pushNotificationService = pushNotificationService;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                Console.WriteLine("OnConnectedAsync called");
                var userId = _authService.GetUserId();
                Console.WriteLine($"User ID retrieved: {userId}");

                var user = await _dbContext.Users.FindAsync(userId);

                if (user != null && user.CoupleId.HasValue)
                {
                    Console.WriteLine($"Adding user {userId} to group {user.CoupleId.Value}");
                    await Groups.AddToGroupAsync(Context.ConnectionId, user.CoupleId.Value.ToString());
                }
                else
                {
                    Console.WriteLine($"User {userId} does not belong to any couple group.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in OnConnectedAsync: {ex.Message}");
                throw;
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                Console.WriteLine("OnDisconnectedAsync called");
                var userId = _authService.GetUserId();
                Console.WriteLine($"User ID retrieved: {userId}");

                var user = await _dbContext.Users.FindAsync(userId);

                if (user != null && user.CoupleId.HasValue)
                {
                    Console.WriteLine($"Removing user {userId} from group {user.CoupleId.Value}");
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, user.CoupleId.Value.ToString());
                }
                else
                {
                    Console.WriteLine($"User {userId} does not belong to any couple group.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in OnDisconnectedAsync: {ex.Message}");
                throw;
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string user, string message)
        {
            var senderId = _authService.GetUserId();
            var sender = await _dbContext.Users.FindAsync(senderId);

            if (sender == null || !sender.CoupleId.HasValue)
            {
                Console.WriteLine("Sender not found or not in a couple.");
                return;
            }

            var coupleId = sender.CoupleId.Value;
            var partner = await _dbContext.Users.FirstOrDefaultAsync(u => u.CoupleId == coupleId && u.Id != senderId);

            if (partner == null)
            {
                Console.WriteLine("Partner not found.");
                return;
            }

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
                        body = $"New message from {user}: {message}"
                    };

                    await _pushNotificationService.SendNotificationAsync(subscription, JsonSerializer.Serialize(payload));
                }
            }

            await Clients.Group(coupleId.ToString()).SendAsync("ReceiveMessage", user, message);
        }
    }
}
