using DateSwipe.Shared;
using DateSwipe.Shared.DTO;

namespace DateSwipe.Server.Services.UserPreferenceService
{
    public interface IUserPreferenceService
    {
        Task<ServiceResponse<bool>> SetCategoryPreferenceAsync(int categoryId, bool liked);
        Task<ServiceResponse<List<UserPreferencesDTO>>> GetLikedCategoriesAsync();
        Task<ServiceResponse<List<UserPreferencesDTO>>> GetAllCategoryPrefernces();

    }
}
