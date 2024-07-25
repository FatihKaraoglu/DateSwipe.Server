using DateSwipe.Server.Services.AuthService;
using DateSwipe.Server.Services.ProfileService;
using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DateSwipe.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IAuthService _authService;
        private readonly IWebHostEnvironment _env;

        public ProfileController(IProfileService profileService, IAuthService authService, IWebHostEnvironment env)
        {
            _profileService = profileService;
            _authService = authService;
            _env = env;
        }

        [HttpGet()]
        public async Task<ServiceResponse<ProfileDTO>> GetProfile()
        {
            var user = await _profileService.GetProfile();
            return user;
        }


        [HttpGet("partner")]
        public async Task<ServiceResponse<ProfileDTO>> GetPartnerProfile()
        {
            var partner = await _profileService.GetPartnerProfile();
            return partner;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] User updatedUser)
        {
            if (updatedUser == null)
            {
                return BadRequest();
            }

            var result = await _profileService.UpdateProfile(updatedUser);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadProfilePicture([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new ServiceResponse<string> { Success = false, Message = "Invalid file." });

            var userId = _authService.GetUserId();
            var uploadsFolderPath = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolderPath);

            var fileName = $"{userId}.{file.FileName}";
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = $"/uploads/{fileName}";
            var result = await _profileService.UpdateProfilePictureUrl(fileUrl);

            if (result.Success)
                return Ok(result);

            return StatusCode(500, new ServiceResponse<string> { Success = false, Message = "Error updating profile picture." });
        }

        [HttpGet("inviterProfile")]
        public async Task<ServiceResponse<ProfileDTO>> GetInviterProfile([FromQuery] string token)
        {
            var inviterProfile = await _profileService.GetInviterProfileAsync(token);
            return inviterProfile;
        }
    }
}
