using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateSwipe.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Role { get; set; } = "Free"; // Default role
        public bool IsSubscribed { get; set; } = false; // Default subscription status
        public int? CoupleId { get; set; } // Couple identifier
        public string? ProfilePictureUrl { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public ICollection<PushSubscription> PushSubscriptions { get; set; }
        public ICollection<UserCategoryPreference> CategoryPreferences { get; set; }
        public List<UserSwipe> UserSwipes { get; set; }
    }
}
