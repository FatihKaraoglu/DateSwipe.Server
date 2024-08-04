using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace DateSwipe.Client.Services.DateProposalService
{
    public class DateProposalService : IDateProposalService
    {
        private readonly HttpClient _httpClient;

        public DateProposalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<DateProposal>> CreateDateProposalAsync(int dateIdeaId, DateTime from, DateTime to)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/dateproposals?dateIdeaId={dateIdeaId}&From={from:s}&To={to:s}", new { });
            return await response.Content.ReadFromJsonAsync<ServiceResponse<DateProposal>>();
        }

        public async Task<ServiceResponse<List<DateProposal>>> GetDateProposalsAsync()
        {
            return await _httpClient.GetFromJsonAsync<ServiceResponse<List<DateProposal>>>("api/dateproposals");
        }

        public async Task<ServiceResponse<bool>> AcceptDateProposalAsync(int proposalId)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<bool>>($"api/dateproposals/accept/{proposalId}");
            return response;
        }

        public async Task<ServiceResponse<bool>> RejectDateProposalAsync(int proposalId)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<bool>>($"api/dateproposals/reject/{proposalId}");
            return response;
        }
    }
}
