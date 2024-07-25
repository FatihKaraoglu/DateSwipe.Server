using DateSwipe.Shared;
using DateSwipe.Shared.DTO;

namespace DateSwipe.Client.Services.ProfileService
{
    public interface IProfileService
    {
        public ProfileDTO User { get; set; }
        public ProfileDTO Partner { get; set; }
        Task<ServiceResponse<ProfileDTO>> GetProfile();
        Task<ServiceResponse<ProfileDTO>> GetPartnerProfile();
        Task<ServiceResponse<ProfileDTO>> GetInviterProfileAsync(string token);
        Task<ServiceResponse<string>> UploadProfilePicture(byte[] file, string fileName);
    }
}
