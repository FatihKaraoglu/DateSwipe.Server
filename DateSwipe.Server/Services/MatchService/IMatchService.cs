using DateSwipe.Shared;

namespace DateSwipe.Server.Services.MatchService
{
    public interface IMatchService
    {
        Task<ServiceResponse<string>> Swipe(int dateIdeaId, bool liked);
    }
}
