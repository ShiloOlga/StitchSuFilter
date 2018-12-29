using Web.Data;
using Web.Data.Entities;

namespace Web.Models
{
    public class FabricItem
    {
        public Fabric Fabric { get; set; }
        public string Sku { get; set; }
    }
}
