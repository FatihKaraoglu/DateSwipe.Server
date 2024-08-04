using DateSwipe.Server.Services.DateProposalService;
using DateSwipe.Shared;
using DateSwipe.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DateSwipe.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DateProposalsController : ControllerBase
    {
        private readonly IDateProposalService _dateProposalService;

        public DateProposalsController(IDateProposalService dateProposalService)
        {
            _dateProposalService = dateProposalService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<DateProposal>>> CreateDateProposal(int dateIdeaId, DateTime From, DateTime To)
        {
            var response = await _dateProposalService.CreateDateProposalAsync(dateIdeaId, From, To);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{proposalId}")]
        public async Task<ActionResult<ServiceResponse<DateProposal>>> GetDateProposal(int proposalId)
        {
            var response = await _dateProposalService.GetDateProposalAsync(proposalId);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("couple/{coupleId}")]
        public async Task<ActionResult<ServiceResponse<List<DateProposal>>>> GetDateProposalsForCouple(int coupleId)
        {
            var response = await _dateProposalService.GetDateProposalsForCoupleAsync(coupleId);
            return Ok(response);
        }

        [HttpGet("accept/{proposalId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> AcceptDateProposal(int proposalId)
        {
            var response = await _dateProposalService.AcceptDateProposalAsync(proposalId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("reject/{proposalId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> RejectDateProposal(int proposalId)
        {
            var response = await _dateProposalService.RejectDateProposalAsync(proposalId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
