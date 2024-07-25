using DateSwipe.Server.Data.DataContext;
using DateSwipe.Server.Services.AuthService;
using DateSwipe.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DateSwipe.Server.Services.ChatService
{
    public class ChatService : IChatService
    {
        private readonly DatingDbContext _context;
        private readonly IAuthService _authService;

        public ChatService(DatingDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task SaveMessageAsync(string message)
        {
            var userId = _authService.GetUserId();
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var chatMessage = new ChatMessage
            {
                UserId = userId,
                CoupleId = user.CoupleId.Value,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChatMessage>> GetMessagesAsync(int coupleId)
        {
            return await _context.ChatMessages
                .Where(m => m.CoupleId == coupleId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }
    }
}
