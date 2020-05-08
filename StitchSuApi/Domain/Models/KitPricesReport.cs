using System.Collections.Generic;
using System.Linq;

namespace StitchSuApi.Domain.Models
{
    public class KitPricesReport
    {
        public IEnumerable<string> ShopNames { get; set; } = Enumerable.Empty<string>();
        public IEnumerable<Kit> Kits { get; set; } = Enumerable.Empty<Kit>();
    }
}
