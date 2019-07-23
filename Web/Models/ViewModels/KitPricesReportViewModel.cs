using System.Collections.Generic;
using System.Linq;
using Web.Models.CrossStitch;

namespace Web.Models.ViewModels
{
    public class KitPricesReportViewModel
    {
        private KitPricesReportViewModel() { }

        public IEnumerable<KitPricesReport> Shops { get; private set; }

        public static KitPricesReportViewModel Build(IEnumerable<WishlistKitModel> sourceItems)
        {
            var items = sourceItems.ToArray();
            var shops = items
                .SelectMany(x => x.AvailableShops)
                .Select(x => x.Name)
                .Distinct()
                .ToArray();
            var model = new KitPricesReportViewModel();
            var list = new List<KitPricesReport>(shops.Length);
            list.AddRange(shops.Select(shop => KitPricesReport.Build(items, shop)));
            model.Shops = list;
            return model;
        }
    }

    public class KitPricesReport
    {
        public string ShopName { get; private set; }
        public string ShopLink { get; private set; }
        public decimal TotalAmount { get; private set; }
        public int TotalCount { get; private set; }
        public IEnumerable<KitPriceReportUnit> Kits { get; private set; }

        private KitPricesReport() { }

        public static KitPricesReport Build(IReadOnlyCollection<WishlistKitModel> sourceItems, string shopName)
        {
            var result = new KitPricesReport();
            var matchingKits = sourceItems
                .Select(x => new
                {
                    Kit = x,
                    Shop = x.AvailableShops.FirstOrDefault(y => y.Name.Equals(shopName))
                })
                .Where(x => x.Shop != null)
                .ToArray();
            result.Kits = matchingKits
                .Select(x => new KitPriceReportUnit
                {
                    Price = x.Shop.Price,
                    Title = x.Kit.Info.Title
                })
                .ToArray();
            result.ShopName = matchingKits.First().Shop.Name;
            result.ShopLink = matchingKits.First().Shop.Link;
            result.TotalCount = result.Kits.Count();
            result.TotalAmount = result.Kits.Sum(x => x.Price);
            return result;
        }
    }

    public class KitPriceReportUnit
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
    }
}
