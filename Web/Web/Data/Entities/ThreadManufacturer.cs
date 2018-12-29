using System.Collections.Generic;

namespace Web.Data.Entities
{
    public partial class ThreadManufacturer
    {
        public ThreadManufacturer()
        {
            Kits = new HashSet<Kit>();
            ThreadColors = new HashSet<ThreadColor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Kit> Kits { get; set; }
        public virtual ICollection<ThreadColor> ThreadColors { get; set; }
    }
}
