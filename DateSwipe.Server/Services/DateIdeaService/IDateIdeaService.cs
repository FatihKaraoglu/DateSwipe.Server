using DateSwipe.Shared;
using DateSwipe.Shared.DTO;

namespace DateSwipe.Server.Services.DateIdeaService
{
    public interface IDateIdeaService
    {
        Task<ServiceResponse<List<DateIdeaDTO>>> GetDateIdeasAsync();
        Task<ServiceResponse<string>> SwipeAsync(int dateId, bool liked);
        Task<ServiceResponse<DateIdeaDTO>> GetDateIdeaByIdAsync(int id);
        Task<ServiceResponse<bool>> DeleteAllSwipesAsync();
        Task<ServiceResponse<List<DateIdeaDTO>>> GetDislikedDateIdeasAsync();
        Task<ServiceResponse<List<DateIdeaDTO>>> GetLikedDateIdeasAsync();
    }
}
