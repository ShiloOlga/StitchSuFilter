using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Web.Data.Entities
{
    public class KitManufacturer
    {
        private readonly ILazyLoader _lazyLoader;
        private ICollection<Kit> _kits;

        public KitManufacturer(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
            _kits = new HashSet<Kit>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Kit> Kits
        {
            get => _lazyLoader.Load(this, ref _kits);
            set => _kits = value;
        }
    }
}
