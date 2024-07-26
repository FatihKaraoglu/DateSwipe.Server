using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Lib.Net.Http.WebPush;
using DateSwipe.Client.Server.PushNotificationService;
using Lib.Net.Http.WebPush.Authentication;
using WebPush;
using DateSwipe.Server.Services.AuthService;
using DateSwipe.Server.Data.DataContext;

namespace DateSwipe.Server.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly PushServiceClient _pushClient;
        private readonly VapidDetails _vapidDetails;
        private readonly IAuthService _authService;
        private readonly DatingDbContext _context;


        public PushNotificationService(IConfiguration configuration)
        {
            _pushClient = new PushServiceClient
            {
                DefaultAuthentication = new VapidAuthentication(
                    configuration["VapidKeys:PublicKey"],
                    configuration["VapidKeys:PrivateKey"])
                {
                    Subject = "mailto:your-email@example.com"
                }
            };
        }

        public async Task SendNotificationAsync(Lib.Net.Http.WebPush.PushSubscription subscription, string payload)
        {
            var message = new PushMessage(payload);
            await _pushClient.RequestPushMessageDeliveryAsync(subscription, message);
        }

        public async Task<bool> SubscribeToPush(bool subscribe)
        {
            var userId = _authService.GetUserId();

            if(userId != null)
            {
                var user = await _context.Users.FindAsync(userId);
                if(user == null)
                {
                    return false;
                }

                user.SubscribedToPush = subscribe;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;   
        }
    }
}
