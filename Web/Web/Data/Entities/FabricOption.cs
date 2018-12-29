using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Web.Data.Entities
{
    public class FabricOption
    {
        private readonly ILazyLoader _lazyLoader;
        private FabricItem _fabricItem;
        private Pattern _pattern;

        public FabricOption(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int PatternId { get; set; }
        public int FabricItemId { get; set; }

        public FabricItem FabricItem
        {
            get => _lazyLoader.Load(this, ref _fabricItem);
            set => _fabricItem = value;
        }

        public Pattern Pattern
        {
            get => _lazyLoader.Load(this, ref _pattern);
            set => _pattern = value;
        }
    }
}
