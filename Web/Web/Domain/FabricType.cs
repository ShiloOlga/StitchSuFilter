using System.Collections.Generic;

namespace Web.Domain
{
    public partial class FabricType
    {
        public FabricType()
        {
            Fabrics = new HashSet<Fabric>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Fabric> Fabrics { get; set; }
    }
}
