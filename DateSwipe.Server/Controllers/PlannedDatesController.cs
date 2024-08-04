using DateSwipe.Server.Services.AuthService;
using DateSwipe.Server.Services.PlannedDateService;
using DateSwipe.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DateSwipe.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlannedDatesController : ControllerBase
    {
        private readonly IPlannedDateService _plannedDateService;
        private readonly IAuthService _authService;

        public PlannedDatesController(IPlannedDateService plannedDateService, IAuthService authService)
        {
            _plannedDateService = plannedDateService;
            _authService = authService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<PlannedDate>>>> GetPlannedDates()
        {
            var response = await _plannedDateService.GetPlannedDatesAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
