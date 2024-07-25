using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using DateSwipe.Shared.DTO;
using DateSwipe.Shared;
using DateSwipe.Shared.RequestObject;

namespace DateSwipe.Client.Services.DateDecisionService
{
    public class DateDecisionService : IDateDecisionService
    {
        private readonly HttpClient _httpClient;

        public DateDecisionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<DateIdeaDTO>>> GetDateIdeasAsync()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true // Optional: Based on your API's JSON naming conventions
            };
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<DateIdeaDTO>>>("api/DateIdeas", options);
            return response;
        }

        public async Task<ServiceResponse<DateIdeaDTO>> GetDateIdeaByIdAsync(int id)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                PropertyNameCaseInsensitive = true // Optional: Based on your API's JSON naming conventions
            };
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<DateIdeaDTO>>($"api/DateIdeas/{id}", options);
            return response;
        }

        public async Task<ServiceResponse<string>> SwipeAsync(SwipeRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Swipes", request);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }
    }
}
