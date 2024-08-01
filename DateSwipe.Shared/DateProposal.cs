using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateSwipe.Shared
{
    public class DateProposal
    {
        public int CoupleId { get; set; }
        public DateIdea DateIdea { get; set; }
        public int DateIdeaId { get; set; }
        public int Partner1 { get; set; }
        public int Partner2 { get; set; }
        public bool Partner1Accept { get; set; }
        public bool Partner2Accept { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime FromTo { get; set; }
    }
}
