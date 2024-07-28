using DateSwipe.Shared;
using DateSwipe.Server.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DateSwipe.Shared.DTO;
using Lib.Net.Http.WebPush.Authentication;
using Lib.Net.Http.WebPush;
using DateSwipe.Server.Services.AuthService;
using DateSwipe.Server.PushNotificationService;

namespace DateSwipe.Server.Services
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly PushServiceClient _pushClient;
        private readonly DatingDbContext _context;
        private readonly IAuthService _authService;

        public PushNotificationService(IConfiguration configuration, DatingDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
            _pushClient = new PushServiceClient
            {
                DefaultAuthentication = new VapidAuthentication(
                    configuration["VapidKeys:PublicKey"],
                    configuration["VapidKeys:PrivateKey"])
                {
                    Subject = "mailto:karaoglu.fatih212000@gmail.com"
                }
            };
        }

        public async Task<ServiceResponse<bool>> SendNotificationAsync(Lib.Net.Http.WebPush.PushSubscription subscription, string payload)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var message = new PushMessage(payload);
                await _pushClient.RequestPushMessageDeliveryAsync(subscription, message);
                response.Data = true;
            }
            catch (PushServiceClientException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Gone)
                {
                    // Remove invalid subscription
                    await RemoveSubscriptionAsync(subscription);
                    response.Success = false;
                    response.Message = "Subscription is no longer valid and has been removed.";
                }
                else
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }

            return response;
        }


        public async Task<ServiceResponse<bool>> AddSubscriptionAsync(PushSubscriptionDTO subscriptionDto)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var userId = _authService.GetUserId();

                var pushSubscription = new Shared.PushSubscription
                {
                    Endpoint = subscriptionDto.Endpoint,
                    P256DH = subscriptionDto.Keys["p256dh"],
                    Auth = subscriptionDto.Keys["auth"],
                    UserId = userId
                };

                _context.PushSubscriptions.Add(pushSubscription);
                await _context.SaveChangesAsync();
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<PushSubscriptionDTO>>> GetSubscriptionsAsync(int userId)
        {
            var response = new ServiceResponse<List<PushSubscriptionDTO>>();

            try
            {
                response.Data = await _context.PushSubscriptions
                    .Where(ps => ps.UserId == userId)
                    .Select(ps => new PushSubscriptionDTO
                    {
                        Endpoint = ps.Endpoint,
                        Keys = new Dictionary<string, string>
                        {
                    { "p256dh", ps.P256DH },
                    { "auth", ps.Auth }
                        }
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task RemoveSubscriptionAsync(Lib.Net.Http.WebPush.PushSubscription subscription)
        {
            var existingSubscription = await _context.PushSubscriptions
                .FirstOrDefaultAsync(s => s.Endpoint == subscription.Endpoint);

            if (existingSubscription != null)
            {
                _context.PushSubscriptions.Remove(existingSubscription);
                await _context.SaveChangesAsync();
            }
        }

    }
}
