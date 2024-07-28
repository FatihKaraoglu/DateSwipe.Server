using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateSwipe.Shared.DTO
{
    public class PushSubscriptionDTO
    {
        public string Endpoint { get; set; }
        public Dictionary<string, string> Keys { get; set; }
    }
}
