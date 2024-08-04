using DateSwipe.Server.Data.DataContext;
using DateSwipe.Server.Services.AuthService;
using DateSwipe.Shared;
using Microsoft.EntityFrameworkCore;

namespace DateSwipe.Server.Services.PlannedDateService
{
    public class PlannedDateService : IPlannedDateService
    {
        private readonly DatingDbContext _dbContext;
        private readonly IAuthService _authService;

        public PlannedDateService(DatingDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<ServiceResponse<List<PlannedDate>>> GetPlannedDatesAsync()
        {
            var response = new ServiceResponse<List<PlannedDate>>();

            var userId = _authService.GetUserId();
            var user = await _dbContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            try
            {
                var plannedDates = await _dbContext.PlannedDates
                    .Include(pd => pd.DateIdea) // Include related DateIdea
                    .Where(pd => pd.CoupleId == user.CoupleId)
                    .ToListAsync();

                response.Data = plannedDates;
                response.Success = true;
                response.Message = "Planned dates retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }
    }
}
