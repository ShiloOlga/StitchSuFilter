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
        public int FabricTypeId { get; set; }
        public int ContentTypeId { get; set; }
        public sbyte Priority { get; set; }

        public ContentType ContentType { get; set; }
        public FabricType FabricType { get; set; }
        public ICollection<FabricItem> FabricItems { get; set; }
    }
}
