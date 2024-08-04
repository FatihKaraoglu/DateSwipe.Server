using DateSwipe.Shared;

namespace DateSwipe.Client.Services.PlannedDateService
{
    public interface IPlannedDateService
    {
        Task<ServiceResponse<List<PlannedDate>>> GetPlannedDatesAsync();
    }
}
