using DateSwipe.Server.Services.AuthService;
using DateSwipe.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DateSwipe.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService AuthService)
        {
            _authService = AuthService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(UserRegister request)
        {
            var response = await _authService.Register(new User
            {
                Email = request.Email,
                Address = request.Address,
                //shouldnt be null cause its checked int the client
                Birthday = (DateTime)request.Birthday,
                Name = request.Name,
                LastName = request.LastName,
                City = request.City,
                
            }, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                var token = await _authService.Login(request.Email, request.Password);

                return token;
                
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
        {
            var response = await _authService.Login(request.Email, request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] string newPassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _authService.ChangePassword(int.Parse(userId), newPassword);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        [HttpGet]
        public async Task<int> GetUserId()
        {
            return _authService.GetUserId();    
        }
    }
}
