using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateSwipe.Shared.DTO
{
    public class ChatMessageDTO
    {
        public string Message { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CoupleId { get; set; }
        public MessageType Type { get; set; }
        public DateIdeaDTO? DateIdea{ get; set; }
        public DateProposalDTO? DateProposal { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
