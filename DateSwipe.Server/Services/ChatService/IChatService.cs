using DateSwipe.Shared;

namespace DateSwipe.Server.Services.ChatService
{
    public interface IChatService
    {
        Task SaveMessageAsync(string message);
        Task<List<ChatMessage>> GetMessagesAsync(int coupleId);
    }
}
