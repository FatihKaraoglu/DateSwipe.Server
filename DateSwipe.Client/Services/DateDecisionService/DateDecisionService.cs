using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using DateSwipe.Shared.DTO;
using DateSwipe.Shared;
using DateSwipe.Shared.RequestObject;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace DateSwipe.Client.Services.DateDecisionService
{
    public class DateDecisionService : IDateDecisionService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ISnackbar _snackbar;

        public DateDecisionService(HttpClient httpClient, NavigationManager navigationManager, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _snackbar = snackbar;
        }

        public async Task<ServiceResponse<List<DateIdeaDTO>>> GetDateIdeasAsync()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            };
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<DateIdeaDTO>>>("api/DateIdeas", options);
            return response;
        }

        public async Task<ServiceResponse<DateIdeaDTO>> GetDateIdeaByIdAsync(int id)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true
            };
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<DateIdeaDTO>>($"api/DateIdeas/{id}", options);
            return response;
        }

        public async Task<ServiceResponse<string>> SwipeAsync(SwipeRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Swipes", request);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<bool>> DeleteAllSwipesAsync()
        {
            var response = await _httpClient.DeleteAsync("api/DateIdeas/swipes");
            var success = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            if (success.Data)
            {
                _navigationManager.NavigateTo("/");
                _snackbar.Add("Successfully deleted all Swipes", Severity.Success);
            }
            else
            {
                _snackbar.Add("Failed to delete all Swipes", Severity.Error);
            }
            return success;
        }

        public async Task<ServiceResponse<List<DateIdeaDTO>>> GetLikedDateIdeasAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<DateIdeaDTO>>>("api/DateIdeas/liked");
            return response;
        }

        public async Task<ServiceResponse<List<DateIdeaDTO>>> GetDislikedDateIdeasAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<DateIdeaDTO>>>("api/DateIdeas/disliked");
            return response;
        }
    }
}
