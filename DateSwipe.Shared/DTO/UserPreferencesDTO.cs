using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateSwipe.Shared.DTO
{
    public class UserPreferencesDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsLiked { get; set; }
    }
}
