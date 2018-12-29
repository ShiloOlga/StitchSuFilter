using System.Collections.Generic;

namespace Web.Data.Entities
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

        public virtual ICollection<Kit> Kits { get; set; }
        public virtual ICollection<Pattern> Patterns { get; set; }
    }
}
