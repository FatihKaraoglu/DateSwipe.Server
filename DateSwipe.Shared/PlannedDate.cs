using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateSwipe.Shared
{
    public class PlannedDate
    {
        public int PlannedDateId { get; set; }
        public int CoupleId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool WholeDay { get; set; }
        public int DateIdeaId { get; set; }
        public DateIdea DateIdea { get; set; }
    }
}
