using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace DateSwipe.Client.Services.DateProposalService
{
    public interface IDateProposalService
    {
        Task<ServiceResponse<DateProposal>> CreateDateProposalAsync(int dateIdeaId, DateTime from, DateTime to);
        Task<ServiceResponse<List<DateProposal>>> GetDateProposalsAsync();
        Task<ServiceResponse<bool>> AcceptDateProposalAsync(int proposalId);
        Task<ServiceResponse<bool>> RejectDateProposalAsync(int proposalId);
    }
}
