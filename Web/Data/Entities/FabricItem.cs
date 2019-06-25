using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Web.Data.Entities
{
    public class FabricItem
    {
        private readonly ILazyLoader _lazyLoader;
        private Fabric _fabric;
        private ICollection<FabricOption> _fabricOptions;
        private ICollection<Kit> _kits;

        public FabricItem(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
            _fabricOptions = new HashSet<FabricOption>();
            _kits = new HashSet<Kit>();
        }

        public int Id { get; set; }
        public int? FabricId { get; set; }
        public string Sku { get; set; }
        public string ColorId { get; set; }
        public string ColorName { get; set; }

        public Fabric Fabric
        {
            get => _lazyLoader.Load(this, ref _fabric);
            set => _fabric = value;
        }

        public ICollection<FabricOption> FabricOptions
        {
            get => _lazyLoader.Load(this, ref _fabricOptions);
            set => _fabricOptions = value;
        }

        public ICollection<Kit> Kits
        {
            get => _lazyLoader.Load(this, ref _kits);
            set => _kits = value;
        }
    }
}
