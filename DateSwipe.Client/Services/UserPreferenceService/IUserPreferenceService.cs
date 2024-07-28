using DateSwipe.Shared;
using DateSwipe.Shared.DTO;

namespace DateSwipe.Client.Services.UserPreferenceService
{
    public interface IUserPreferenceService
    {
        List<UserPreferencesDTO> UserPreferences { get; set; }
        Task<ServiceResponse<List<UserPreferencesDTO>>> GetAllCategoryPrefernces();
        Task<ServiceResponse<List<UserPreferencesDTO>>> GetLikedCategoriesAsync();
        Task<ServiceResponse<bool>> SetCategoryPreferenceAsync(int categoryId, bool liked);
    }
}
