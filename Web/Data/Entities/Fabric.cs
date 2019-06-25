using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Web.Data.Entities
{
    public class Fabric
    {
        private readonly ILazyLoader _lazyLoader;
        private ICollection<FabricItem> _fabricItems;
        private FabricType _fabricType;
        private ContentType _contentType;

        public Fabric(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
            _fabricItems = new HashSet<FabricItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public sbyte Count { get; set; }
        public int? FabricTypeId { get; set; }
        public int? ContentTypeId { get; set; }
        public sbyte Priority { get; set; }

        public ContentType ContentType
        {
            get => _lazyLoader.Load(this, ref _contentType);
            set => _contentType = value;
        }

        public FabricType FabricType
        {
            get => _lazyLoader.Load(this, ref _fabricType);
            set => _fabricType = value;
        }

        public ICollection<FabricItem> FabricItems
        {
            get => _lazyLoader.Load(this, ref _fabricItems);
            set => _fabricItems = value;
        }
    }
}
