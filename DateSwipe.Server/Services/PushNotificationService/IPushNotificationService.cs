using Lib.Net.Http.WebPush;

namespace DateSwipe.Client.Server.PushNotificationService
{
    public interface IPushNotificationService
    {
        Task SendNotificationAsync(PushSubscription subscription, string payload);
    }
}
