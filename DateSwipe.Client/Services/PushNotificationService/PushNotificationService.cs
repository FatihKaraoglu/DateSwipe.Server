using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DateSwipe.Client.Services.PushNotificationService
{
    public class PushNotificationService : IPushNotificationService
    {
        private readonly HttpClient _httpClient;

        public PushNotificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<bool>> SubscribeAsync(PushSubscriptionDTO subscriptionDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/PushNotification/subscribe", subscriptionDto);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<List<PushSubscriptionDTO>>> GetSubscriptionsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<PushSubscriptionDTO>>>("api/PushNotification/subscriptions");
            return response;
        }

        public async Task<ServiceResponse<bool>> SendNotificationAsync(List<PushSubscriptionDTO> subscriptions, string message)
        {
            var request = new SendNotificationRequestObject
            {
                Subscriptions = subscriptions,
                Message = message
            };

            var response = await _httpClient.PostAsJsonAsync("api/PushNotification/send", request);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }

    public class SendNotificationRequestObject
    {
        public List<PushSubscriptionDTO> Subscriptions { get; set; }
        public string Message { get; set; }
    }
}
