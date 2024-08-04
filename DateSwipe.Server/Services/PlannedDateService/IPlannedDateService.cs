using DateSwipe.Shared;

namespace DateSwipe.Server.Services.PlannedDateService
{
    public interface IPlannedDateService
    {
        Task<ServiceResponse<List<PlannedDate>>> GetPlannedDatesAsync();
    }
}
