using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using Lib.Net.Http.WebPush;

namespace DateSwipe.Server.PushNotificationService
{
    public interface IPushNotificationService
    {
        Task<ServiceResponse<bool>> SendNotificationAsync(Lib.Net.Http.WebPush.PushSubscription subscription, string payload);
        Task<ServiceResponse<bool>> AddSubscriptionAsync(PushSubscriptionDTO subscriptionDto);
        Task<ServiceResponse<List<PushSubscriptionDTO>>> GetSubscriptionsAsync(int userId);
    }
}
