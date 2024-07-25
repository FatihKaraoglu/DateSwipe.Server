using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace DateSwipe.Client.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        public ProfileDTO User { get; set; }
        public ProfileDTO Partner { get; set; }
        private readonly HttpClient _httpClient;
        public ProfileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<ProfileDTO>> GetPartnerProfile()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<ProfileDTO>>("api/profile/partner");
            Partner = response.Data;
            return response;
        }

        public async Task<ServiceResponse<ProfileDTO>> GetProfile()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<ProfileDTO>>("api/profile");
            User = response.Data;
            return response;
        }

        public async Task<ServiceResponse<string>> UploadProfilePicture(byte[] file, string fileName)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new ByteArrayContent(file);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
            content.Add(fileContent, "file", fileName);

            var response = await _httpClient.PostAsync("api/profile/upload", content);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();

            return result;
        }

        public async Task<ServiceResponse<ProfileDTO>> GetInviterProfileAsync(string token)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<ProfileDTO>>($"api/profile/inviterProfile?token={token}");
            return response;
        }
    }
}
