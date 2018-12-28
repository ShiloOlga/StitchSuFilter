using System.Collections.Generic;

namespace Web.Domain
{
    public partial class FabricItem
    {
        public FabricItem()
        {
            FabricOptions = new HashSet<FabricOption>();
            Kits = new HashSet<Kit>();
        }

        public int Id { get; set; }
        public int? FabricId { get; set; }
        public string Sku { get; set; }
        public string ColorId { get; set; }
        public string ColorName { get; set; }

        public Fabric Fabric { get; set; }
        public ICollection<FabricOption> FabricOptions { get; set; }
        public ICollection<Kit> Kits { get; set; }
    }
}
