using DateSwipe.Shared;
using DateSwipe.Shared.DTO;

namespace DateSwipe.Client.Services.PushNotificationService
{
    public interface IPushNotificationService
    {
        Task<ServiceResponse<bool>> SubscribeAsync(PushSubscriptionDTO subscriptionDto);
        Task<ServiceResponse<List<PushSubscriptionDTO>>> GetSubscriptionsAsync();
        Task<ServiceResponse<bool>> SendNotificationAsync(List<PushSubscriptionDTO> subscriptions, string message);
    }
}
