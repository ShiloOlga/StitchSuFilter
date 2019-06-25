using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Web.Data.Entities
{
    public class Pattern
    {
        private readonly ILazyLoader _lazyLoader;
        private ICollection<FabricOption> _fabricOptions;
        private ICollection<ThreadColorOption> _threadColorOptions;

        private PatternAuthor _author;

        public Pattern(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
            _fabricOptions = new HashSet<FabricOption>();
            _threadColorOptions = new HashSet<ThreadColorOption>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Item { get; set; }
        public int ColorsCount { get; set; }
        public short Width { get; set; }
        public short Height { get; set; }
        public int? AuthorId { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }

        public PatternAuthor Author
        {
            get => _lazyLoader.Load(this, ref _author);
            set => _author = value;
        }

        public ICollection<FabricOption> FabricOptions
        {
            get => _lazyLoader.Load(this, ref _fabricOptions);
            set => _fabricOptions = value;
        }

        public ICollection<ThreadColorOption> ThreadColorOptions
        {
            get => _lazyLoader.Load(this, ref _threadColorOptions);
            set => _threadColorOptions  = value;
        }
    }
}
