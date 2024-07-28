using DateSwipe.Server.Services.DateIdeaService;
using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DateSwipe.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateIdeasController : ControllerBase
    {
        private readonly IDateIdeaService _dateIdeaService;

        public DateIdeasController(IDateIdeaService dateIdeaService)
        {
            _dateIdeaService = dateIdeaService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<DateIdeaDTO>>>> GetDateIdeas()
        {
            var response = await _dateIdeaService.GetDateIdeasAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<DateIdeaDTO>>> GetDateIdeaById(int id)
        {
            var response = await _dateIdeaService.GetDateIdeaByIdAsync(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("swipes")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteAllSwipes()
        {
            var response = await _dateIdeaService.DeleteAllSwipesAsync();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("liked")]
        public async Task<ActionResult<ServiceResponse<List<DateIdeaDTO>>>> GetLikedDateIdeas()
        {
            var response = await _dateIdeaService.GetLikedDateIdeasAsync();
            return Ok(response);
        }

        [HttpGet("disliked")]
        public async Task<ActionResult<ServiceResponse<List<DateIdeaDTO>>>> GetDislikedDateIdeas()
        {
            var response = await _dateIdeaService.GetDislikedDateIdeasAsync();
            return Ok(response);
        }
    }
}
