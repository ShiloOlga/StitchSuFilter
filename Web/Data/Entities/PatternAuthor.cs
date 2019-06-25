using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Web.Data.Entities
{
    public class PatternAuthor
    {
        private readonly ILazyLoader _lazyLoader;
        private ICollection<Kit> _kits;
        private ICollection<Pattern> _patterns;

        public PatternAuthor(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
            _kits = new HashSet<Kit>();
            _patterns = new HashSet<Pattern>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Kit> Kits
        {
            get => _lazyLoader.Load(this, ref _kits);
            set => _kits = value;
        }

        public ICollection<Pattern> Patterns
        {
            get => _lazyLoader.Load(this, ref _patterns);
            set => _patterns = value;
        }
    }
}
