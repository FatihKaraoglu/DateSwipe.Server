namespace DateSwipe.Server.Hub
{
    using DateSwipe.Server.Data.DataContext;
    using DateSwipe.Server.Services.AuthService;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class ChatHub : Hub
    {
        private readonly IAuthService _authService;
        private readonly DatingDbContext _dbContext;

        public ChatHub(IAuthService authService, DatingDbContext dbContext)
        {
            _authService = authService;
            _dbContext = dbContext;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                Console.WriteLine("OnConnectedAsync called");
                var userId = _authService.GetUserId();
                Console.WriteLine($"User ID retrieved: {userId}");

                var user = await _dbContext.Users.FindAsync(userId);

                if (user != null && user.CoupleId.HasValue)
                {
                    Console.WriteLine($"Adding user {userId} to group {user.CoupleId.Value}");
                    await Groups.AddToGroupAsync(Context.ConnectionId, user.CoupleId.Value.ToString());
                }
                else
                {
                    Console.WriteLine($"User {userId} does not belong to any couple group.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in OnConnectedAsync: {ex.Message}");
                throw;
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                Console.WriteLine("OnDisconnectedAsync called");
                var userId = _authService.GetUserId();
                Console.WriteLine($"User ID retrieved: {userId}");

                var user = await _dbContext.Users.FindAsync(userId);

                if (user != null && user.CoupleId.HasValue)
                {
                    Console.WriteLine($"Removing user {userId} from group {user.CoupleId.Value}");
                    await Groups.RemoveFromGroupAsync(Context.ConnectionId, user.CoupleId.Value.ToString());
                }
                else
                {
                    Console.WriteLine($"User {userId} does not belong to any couple group.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in OnDisconnectedAsync: {ex.Message}");
                throw;
            }

            await base.OnDisconnectedAsync(exception);
        }
    }

}
