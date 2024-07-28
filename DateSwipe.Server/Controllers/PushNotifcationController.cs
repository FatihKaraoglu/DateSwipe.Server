using DateSwipe.Server.PushNotificationService;
using DateSwipe.Server.Services.AuthService;
using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using DateSwipe.Shared.RequestObject;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace DateSwipe.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PushNotificationController : ControllerBase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IAuthService _authService;

        public PushNotificationController(IPushNotificationService pushNotificationService, IAuthService authService)
        {
            _pushNotificationService = pushNotificationService;
            _authService = authService;
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] PushSubscriptionDTO subscriptionDto)
        {
            var response = await _pushNotificationService.AddSubscriptionAsync(subscriptionDto);
            return Ok(response);
        }

        [HttpGet("subscriptions")]
        public async Task<IActionResult> GetSubscriptions()
        {
            int userId = _authService.GetUserId();

            if(userId == null)
            {
                Console.WriteLine("UserId Claim is missing!");
                return BadRequest();
            }
            var response = await _pushNotificationService.GetSubscriptionsAsync(userId);
            return Ok(response);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequestObject request)
        {
            var response = new ServiceResponse<bool>();

            foreach (var subscriptionDto in request.Subscriptions)
            {
                var subscription = new Lib.Net.Http.WebPush.PushSubscription
                {
                    Endpoint = subscriptionDto.Endpoint,
                    Keys = subscriptionDto.Keys
                };

                var result = await _pushNotificationService.SendNotificationAsync(subscription, request.Message);

                if (!result.Success)
                {
                    response.Success = false;
                    response.Message = result.Message;
                    return BadRequest(response);
                }
            }

            response.Data = true;
            return Ok(response);
        }

        [HttpPost("send-test")]
        public async Task<IActionResult> SendTestNotification([FromBody] string message)
        {
            int userId = _authService.GetUserId();

            if (userId == null)
            {
                Console.WriteLine("UserId Claim is missing!");
                return BadRequest();
            }
            var subscriptionsResponse = await _pushNotificationService.GetSubscriptionsAsync(userId);
            if (!subscriptionsResponse.Success || subscriptionsResponse.Data == null || subscriptionsResponse.Data.Count == 0)
            {
                return BadRequest("No subscriptions found.");
            }

            var response = new ServiceResponse<bool>();
            foreach (var subscriptionDto in subscriptionsResponse.Data)
            {
                var subscription = new Lib.Net.Http.WebPush.PushSubscription
                {
                    Endpoint = subscriptionDto.Endpoint,
                    Keys = subscriptionDto.Keys
                };

                var payload = new
                {
                    title = "Test Notification",
                    body = message
                };

                var result = await _pushNotificationService.SendNotificationAsync(subscription, JsonSerializer.Serialize(payload));

                if (!result.Success && result.Message == "Subscription is no longer valid and has been removed.")
                {
                    // Log or handle the removal of the invalid subscription
                    Console.WriteLine("Removed invalid subscription: " + subscription.Endpoint);
                    continue;
                }
                else if (!result.Success)
                {
                    response.Success = false;
                    response.Message = result.Message;
                    return BadRequest(response);
                }
            }

            response.Data = true;
            return Ok(response);
        }


    }
}
