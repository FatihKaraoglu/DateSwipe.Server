using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DateSwipe.Server.Services.DateProposalService
{
    public interface IDateProposalService
    {
        Task<ServiceResponse<DateProposal>> CreateDateProposalAsync(int DateIdeaId, DateTime From, DateTime To);
        Task<ServiceResponse<DateProposal>> GetDateProposalAsync(int proposalId);
        Task<ServiceResponse<List<DateProposal>>> GetDateProposalsForCoupleAsync(int coupleId);
        Task<ServiceResponse<bool>> AcceptDateProposalAsync(int proposalId);
        Task<ServiceResponse<bool>> RejectDateProposalAsync(int proposalId);
    }
}
