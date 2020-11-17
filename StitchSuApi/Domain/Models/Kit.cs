using System.Collections.Generic;
using System.Linq;

namespace StitchSuApi.Domain.Models
{
    public class Kit
    {
        public string Title { get; set; }
        public string Manufacturer { get; set; }
        public string PreviewUrl { get; set; }
        public IEnumerable<Shop> Shops { get; set; } = Enumerable.Empty<Shop>();
    }
}
