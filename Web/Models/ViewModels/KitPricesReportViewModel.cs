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
                    Shop = x.AvailableShops.FirstOrDefault(y => y.Name.Equals(shopName)),
                    MinPrice = x.AvailableShops.DefaultIfEmpty().Min(y => y?.Price ?? 0)
                })
                .Where(x => x.Shop != null)
                .ToArray();
            result.Kits = matchingKits
                .Select(x => new KitPriceReportUnit
                {
                    Price = x.Shop.Price,
                    Title = x.Kit.Info.Title,
                    IsTop = x.Kit.Tags.Any(y => y.Name == "Top kit"),
                    Place = x.Kit.AvailableShops.Count(y => x.Shop.Price > y.Price) + 1,
                    Url = x.Kit.Image.PreviewImageUrl,
                    Delta = x.Shop.Price - x.MinPrice
                })
                .OrderBy(x => x.IsTop ? 0 : 1)
                .ThenBy(x => x.Place)
                .ThenBy(x => x.Price)
                .ToArray();
            result.ShopName = matchingKits.First().Shop.Name;
            result.ShopLink = matchingKits.First().Shop.Link;
            result.TotalCount = result.Kits.Count();
            result.TotalAmount = result.Kits.Sum(x => x.Price);
            return result;
        }

        private static int CompareDinosByLength(string x, string y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    int retval = x.Length.CompareTo(y.Length);

                    if (retval != 0)
                    {
                        // If the strings are not of equal length,
                        // the longer string is greater.
                        //
                        return retval;
                    }
                    else
                    {
                        // If the strings are of equal length,
                        // sort them with ordinary string comparison.
                        //
                        return x.CompareTo(y);
                    }
                }
            }
        }
    }

    public class KitPriceReportUnit
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsTop { get; set; }
        public decimal Price { get; set; }
        public decimal Delta { get; set; }
        public int Place { get; set; }
    }
}
