using Web.Data.Entities;

namespace Web.Models
{
    public class FabricItemModel
    {
        public Fabric Fabric { get; set; }
        public string Sku { get; set; }
        public string ColorId { get; set; }
        public string ColorName { get; set; }

        public override string ToString()
        {
            return $"{Fabric.Name}, count {Fabric.Count}, {Sku}/{ColorId} - {ColorName}";
        }
    }
}
