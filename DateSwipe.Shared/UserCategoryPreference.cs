using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateSwipe.Shared
{
    public class UserCategoryPreference
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public bool Liked { get; set; } 
    }

}
