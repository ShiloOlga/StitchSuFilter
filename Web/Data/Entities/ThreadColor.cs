using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Web.Data.Entities
{
    public class ThreadColor
    {
        private readonly ILazyLoader _lazyLoader;
        private ThreadManufacturer _manufacturer;
        private ICollection<ThreadColorOption> _threadColorOptions;

        public ThreadColor(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
            _threadColorOptions = new HashSet<ThreadColorOption>();
        }

        public int Id { get; set; }
        public int? ManufacturerId { get; set; }
        public string Sku { get; set; }
        public string ColorId { get; set; }
        public string ColorName { get; set; }
        public string RgbColor { get; set; }
        public sbyte Length { get; set; }

        public ThreadManufacturer Manufacturer
        {
            get => _lazyLoader.Load(this, ref _manufacturer);
            set => _manufacturer = value;
        }

        public ICollection<ThreadColorOption> ThreadColorOptions
        {
            get => _lazyLoader.Load(this, ref _threadColorOptions);
            set => _threadColorOptions = value;
        }
    }
}
