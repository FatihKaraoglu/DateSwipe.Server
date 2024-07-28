using DateSwipe.Server.Services.UserPreferenceService;
using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DateSwipe.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreferenceController : ControllerBase
    {
        private readonly IUserPreferenceService _userPreferenceService;

        public UserPreferenceController(IUserPreferenceService userPreferenceService)
        {
            _userPreferenceService = userPreferenceService;
        }

        [HttpGet("preferences")]
        public async Task<ActionResult<ServiceResponse<List<UserPreferencesDTO>>>> GetAllCategoryPreferences()
        {
            var response = await _userPreferenceService.GetAllCategoryPrefernces();
            return Ok(response);
        }

        [HttpGet("liked")]
        public async Task<ActionResult<ServiceResponse<List<UserPreferencesDTO>>>> GetLikedCategories()
        {
            var response = await _userPreferenceService.GetLikedCategoriesAsync();
            return Ok(response);
        }

        [HttpPost("set")]
        public async Task<ActionResult<ServiceResponse<bool>>> SetCategoryPreference([FromBody] UserPreferenceUpdateRequest request)
        {
            var response = await _userPreferenceService.SetCategoryPreferenceAsync(request.CategoryId, request.IsLiked);
            return Ok(response);
        }
    }

    public class UserPreferenceUpdateRequest
    {
        public int CategoryId { get; set; }
        public bool IsLiked { get; set; }
    }
}

