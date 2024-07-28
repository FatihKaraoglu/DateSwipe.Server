using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateSwipe.Shared
{
    public class PushSubscription
    {
        public int Id { get; set; }
        public string Endpoint { get; set; }
        public string P256DH { get; set; }
        public string Auth { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
