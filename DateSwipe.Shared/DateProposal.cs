using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DateSwipe.Shared
{
    public class DateProposal
    {
        public int Id { get; set; } // Added Id for primary key
        public int CoupleId { get; set; }
        public DateIdea DateIdea { get; set; }
        public int DateIdeaId { get; set; }
        public bool? Accept { get; set; }
        public bool Canceled { get; set; } = false;
        public int DateProposalIssuer { get; set; } // UserId of the issuer
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public DateTime TimeStamp { get; set; }

    }
}

