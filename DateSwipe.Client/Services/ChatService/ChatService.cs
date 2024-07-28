using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using DateSwipe.Shared;
using DateSwipe.Client.Services.AuthService;
using System.Text.Json.Serialization;
using DateSwipe.Client.Services.PushNotificationService;

namespace DateSwipe.Client.Services.ChatService
{
    public class ChatService
    {
        private readonly NavigationManager _navigationManager;
        private readonly ILogger<ChatService> _logger;
        private readonly HttpClient _httpClient;
        private HubConnection _hubConnection;
        private bool _isConnected;
        private readonly IAuthService _authService;

        public event Func<string, string, Task> OnMessageReceived;

        public ChatService(NavigationManager navigationManager, ILogger<ChatService> logger, HttpClient httpClient, IAuthService authService)
        {
            _navigationManager = navigationManager;
            _logger = logger;
            _httpClient = httpClient;
            _authService = authService;
        }

        public async Task StartAsync()
        {
            if (_isConnected) return;

            var token = await _authService.GetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                _logger.LogError("Token is missing, cannot establish SignalR connection.");
                return;
            }

            _hubConnection = new HubConnectionBuilder()
                .WithUrl(_navigationManager.ToAbsoluteUri("/chatHub"), options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(token);
                })
                .Build();

            _hubConnection.On<string, string>("ReceiveMessage", async (user, message) =>
            {
                if (OnMessageReceived != null)
                {
                    await OnMessageReceived.Invoke(user, message);
                }
            });

            _hubConnection.Closed += async (error) =>
            {
                _logger.LogError($"SignalR connection closed: {error?.Message}");
                _isConnected = false;
                await Task.Delay(2000);
                await StartAsync();
            };

            try
            {
                await _hubConnection.StartAsync();
                _isConnected = true;
                _logger.LogInformation("SignalR Connected");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error starting SignalR connection: {ex.Message}");
                await Task.Delay(2000);
                await StartAsync();
            }
        }

        public async Task SendMessage(string message)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/api/ChatMessages", message);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error saving message to server.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception sending message: {ex.Message}");
            }
        }

        public async Task<List<ChatMessage>> GetChatMessagesAsync()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true // Optional: Based on your API's JSON naming conventions
                };
                var response = await _httpClient.GetFromJsonAsync<List<ChatMessage>>("api/chatMessages", options);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception fetching chat messages: {ex.Message}");
                return new List<ChatMessage>();
            }
        }

        public async Task StopAsync()
        {
            if (_hubConnection != null && _isConnected)
            {
                await _hubConnection.StopAsync();
                _isConnected = false;
            }
        }
    }
}
