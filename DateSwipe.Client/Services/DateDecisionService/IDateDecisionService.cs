using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using DateSwipe.Shared.RequestObject;

namespace DateSwipe.Client.Services.DateDecisionService
{
    public interface IDateDecisionService
    {
        Task<ServiceResponse<List<DateIdeaDTO>>> GetDateIdeasAsync();
        Task<ServiceResponse<string>> SwipeAsync(SwipeRequest request);
        Task<ServiceResponse<DateIdeaDTO>> GetDateIdeaByIdAsync(int id);
    }
}
