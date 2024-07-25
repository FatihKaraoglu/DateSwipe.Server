using DateSwipe.Shared;
using DateSwipe.Shared.DTO;

namespace DateSwipe.Server.Services.ProfileService
{
    public interface IProfileService
    {
        Task<ServiceResponse<ProfileDTO>> GetProfile();
        Task<ServiceResponse<ProfileDTO>> GetPartnerProfile();
        Task<ServiceResponse<string>> UpdateProfilePictureUrl(string fileUrl);
        Task<ServiceResponse<ProfileDTO>> GetInviterProfileAsync(string token);
        Task<bool> UpdateProfile(User updatedUser);
    }
}
