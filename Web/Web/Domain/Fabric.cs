using System.Collections.Generic;

namespace Web.Domain
{
    public partial class Fabric
    {
        public Fabric()
        {
            FabricItems = new HashSet<FabricItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public sbyte Count { get; set; }
        public int? FabricTypeId { get; set; }
        public int? ContentTypeId { get; set; }
        public sbyte Priority { get; set; }

        public virtual ContentType ContentType { get; set; }
        public virtual FabricType FabricType { get; set; }
        public virtual ICollection<FabricItem> FabricItems { get; set; }
    }
}
