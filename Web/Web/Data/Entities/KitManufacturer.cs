using System.Collections.Generic;

namespace Web.Data.Entities
{
    public partial class KitManufacturer
    {
        public KitManufacturer()
        {
            Kits = new HashSet<Kit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Kit> Kits { get; set; }
    }
}
