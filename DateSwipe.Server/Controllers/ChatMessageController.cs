using DateSwipe.Server.Data.DataContext;
using DateSwipe.Server.Hub;
using DateSwipe.Server.Services.AuthService;
using DateSwipe.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DateSwipe.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly DatingDbContext _context;
        private readonly IAuthService _authService;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatMessagesController(DatingDbContext context, IAuthService authService, IHubContext<ChatHub> hubContext)
        {
            _context = context;
            _authService = authService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SaveMessage([FromBody] string messageContent)
        {
            var userId = _authService.GetUserId();
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var chatMessage = new ChatMessage
            {
                UserId = userId,
                CoupleId = user.CoupleId.Value,
                Message = messageContent,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            // Notify the other user in the couple
            var coupleId = user.CoupleId.Value;
            await _hubContext.Clients.Group(coupleId.ToString()).SendAsync("ReceiveMessage", user.Name, messageContent);

            return Ok(chatMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var userId = _authService.GetUserId();
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            var messages = await _context.ChatMessages
                .Where(m => m.CoupleId == user.CoupleId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();

            return Ok(messages);
        }
    }
}
