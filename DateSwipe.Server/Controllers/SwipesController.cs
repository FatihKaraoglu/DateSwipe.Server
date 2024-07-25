using DateSwipe.Server.Hub;
using DateSwipe.Server.Services.MatchService;
using DateSwipe.Shared;
using DateSwipe.Shared.RequestObject;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DateSwipe.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SwipesController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public SwipesController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<string>>> Swipe([FromBody] SwipeRequest request)
        {
            if (request == null || request.DateId <= 0)
            {
                return BadRequest("Invalid request data");
            }

            var response = await _matchService.Swipe(request.DateId ,request.Liked);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }





}
