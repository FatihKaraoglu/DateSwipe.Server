using DateSwipe.Shared.DTO;
using DateSwipe.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DateSwipe.Client.Services.UserPreferenceService
{
    public class UserPreferenceService : IUserPreferenceService
    {
        private readonly HttpClient _httpClient;
        public List<UserPreferencesDTO> UserPreferences { get; set; }

        public UserPreferenceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<UserPreferencesDTO>>> GetAllCategoryPrefernces()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<UserPreferencesDTO>>>("api/UserPreference/preferences");
            UserPreferences = response.Data;
            return response;
        }

        public async Task<ServiceResponse<List<UserPreferencesDTO>>> GetLikedCategoriesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<UserPreferencesDTO>>>("api/UserPreference/liked");
            return response;
        }

        public async Task<ServiceResponse<bool>> SetCategoryPreferenceAsync(int categoryId, bool liked)
        {
            var request = new UserPreferenceUpdateRequest
            {
                CategoryId = categoryId,
                IsLiked = liked
            };

            var response = await _httpClient.PostAsJsonAsync("api/UserPreference/set", request);
            return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }

    public class UserPreferenceUpdateRequest
    {
        public int CategoryId { get; set; }
        public bool IsLiked { get; set; }
    }
}
