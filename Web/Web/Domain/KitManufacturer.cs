using System.Collections.Generic;

namespace Web.Domain
{
    public partial class KitManufacturer
    {
        public KitManufacturer()
        {
            Kits = new HashSet<Kit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Kit> Kits { get; set; }
    }
}
