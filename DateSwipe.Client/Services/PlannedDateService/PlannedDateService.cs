using DateSwipe.Shared;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DateSwipe.Client.Services.PlannedDateService
{
    public class PlannedDateService : IPlannedDateService
    {
        private readonly HttpClient _httpClient;

        public PlannedDateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<List<PlannedDate>>> GetPlannedDatesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<PlannedDate>>>("api/planneddates");
                return response;
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request exception
                return new ServiceResponse<List<PlannedDate>>
                {
                    Data = null,
                    Success = false,
                    Message = "Error occurred while fetching planned dates."
                };
            }
        }
    }
}
