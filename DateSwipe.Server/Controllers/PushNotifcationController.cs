using Lib.Net.Http.WebPush;
using Microsoft.AspNetCore.Mvc;
using DateSwipe.Server.Services;
using System.Threading.Tasks;
using DateSwipe.Client.Server.PushNotificationService;

namespace DateSwipe.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PushNotificationController : ControllerBase
    {
        private readonly IPushNotificationService _pushNotificationService;

        public PushNotificationController(IPushNotificationService pushNotificationService)
        {
            _pushNotificationService = pushNotificationService;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] PushSubscription subscription)
        {
            // Store the subscription in your database for later use
            // Here you should save the subscription details to a persistent storage
            // for simplicity, we'll just return OK

            return Ok();
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] string message)
        {
            // Retrieve the subscriptions from your database and send the notification
            // For simplicity, we'll assume you have a method to get all subscriptions
            var subscriptions = await GetSubscriptionsAsync();

            foreach (var subscription in subscriptions)
            {
                await _pushNotificationService.SendNotificationAsync(subscription, message);
            }

            return Ok();
        }

        private Task<List<PushSubscription>> GetSubscriptionsAsync()
        {
            // Implement your logic to retrieve subscriptions from the database
            // For example, you could use Entity Framework to get subscriptions from a SQL database

            return Task.FromResult(new List<PushSubscription>());
        }
    }
}
