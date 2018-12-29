using System.Collections.Generic;

namespace Web.Domain
{
    public partial class ContentType
    {
        public ContentType()
        {
            Fabrics = new HashSet<Fabric>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Fabric> Fabrics { get; set; }
    }
}
