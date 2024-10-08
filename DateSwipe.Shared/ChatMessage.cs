﻿using DateSwipe.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateSwipe.Shared
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int CoupleId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public MessageType Type { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public enum MessageType
    {
        User,
        Match,
        DateProposal
    }
}
