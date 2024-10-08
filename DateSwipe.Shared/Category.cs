﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateSwipe.Shared
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DateIdeaCategory> DateIdeaCategories { get; set; }
        public ICollection<UserCategoryPreference> UserPreferences { get; set; }
    }
}
