using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Web.Data.Entities
{
    public class ThreadManufacturer
    {
        private readonly ILazyLoader _lazyLoader;
        private ICollection<Kit> _kits;
        private ICollection<ThreadColor> _threadColors;

        public ThreadManufacturer(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
            _kits = new HashSet<Kit>();
            _threadColors = new HashSet<ThreadColor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Kit> Kits
        {
            get => _lazyLoader.Load(this, ref _kits);
            set => _kits = value;
        }

        public ICollection<ThreadColor> ThreadColors
        {
            get => _lazyLoader.Load(this, ref _threadColors);
            set => _threadColors = value;
        }
    }
}
