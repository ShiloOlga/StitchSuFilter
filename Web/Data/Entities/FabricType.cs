using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Web.Data.Entities
{
    public class FabricType
    {
        private readonly ILazyLoader _lazyLoader;
        private ICollection<Fabric> _fabrics;

        public FabricType(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
            _fabrics = new HashSet<Fabric>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Fabric> Fabrics
        {
            get => _lazyLoader.Load(this, ref _fabrics);
            set => _fabrics = value;
        }
    }
}
