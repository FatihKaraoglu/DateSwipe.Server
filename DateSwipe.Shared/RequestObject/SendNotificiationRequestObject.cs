using DateSwipe.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateSwipe.Shared.RequestObject
{
    public class SendNotificationRequestObject
    {
        public List<PushSubscriptionDTO> Subscriptions { get; set; }
        public string Message { get; set; }
    }
}
