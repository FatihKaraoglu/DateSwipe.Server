using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DateSwipe.Shared.DTO
{
    public class ProfileDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
        public string Role { get; set; }
        public int? CoupleId { get; set; }
        public string? ProfilePicture { get; set; }
        public List<UserSwipe> UserSwipes { get; set; }
    }
}
