using DateSwipe.Shared;

namespace DateSwipe.Client.Services.InvitationService
{
    public interface IInvitationService
    {
        Task<ServiceResponse<string>> GenerateInvitationLinkAsync();
        Task<ServiceResponse<string>> AcceptInvitationAsync(string token);
        Task<ServiceResponse<bool>> LeaveCoupleAsync();
        Task<ServiceResponse<bool>> IsUserInCouple();
    }
}
