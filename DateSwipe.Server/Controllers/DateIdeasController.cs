using DateSwipe.Server.Data.DataContext;
using DateSwipe.Server.Services.DateIdeaService;
using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }



}
