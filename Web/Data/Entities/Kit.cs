using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Web.Data.Entities
{
    public class Kit
    {
        private readonly ILazyLoader _lazyLoader;
        private PatternAuthor _author;
        private FabricItem _fabricItem;
        private KitManufacturer _manufacturer;
        private ThreadManufacturer _threadManufacturer;

        public Kit(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }

        public int Id { get; set; }
        public int? ManufacturerId { get; set; }
        public string Title { get; set; }
        public string Item { get; set; }
        public int? ThreadManufacturerId { get; set; }
        public int? FabricItemId { get; set; }
        public int ColorsCount { get; set; }
        public decimal WidthSm { get; set; }
        public decimal HeightSm { get; set; }
        public short WidthStitches { get; set; }
        public short HeightStitches { get; set; }
        public int? AuthorId { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }

        public PatternAuthor Author
        {
            get => _lazyLoader.Load(this, ref _author);
            set => _author = value;
        }

        public FabricItem FabricItem
        {
            get => _lazyLoader.Load(this, ref _fabricItem);
            set => _fabricItem = value;
        }

        public KitManufacturer Manufacturer
        {
            get => _lazyLoader.Load(this, ref _manufacturer);
            set => _manufacturer = value;
        }

        public ThreadManufacturer ThreadManufacturer
        {
            get => _lazyLoader.Load(this, ref _threadManufacturer);
            set => _threadManufacturer = value;
        }
    }
}
