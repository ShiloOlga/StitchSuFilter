﻿using System.Collections.Generic;

namespace Web.Domain
{
    public partial class PatternAuthor
    {
        public PatternAuthor()
        {
            Kits = new HashSet<Kit>();
            Patterns = new HashSet<Pattern>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Kit> Kits { get; set; }
        public ICollection<Pattern> Patterns { get; set; }
    }
}
