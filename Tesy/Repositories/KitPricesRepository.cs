using System;
using System.Linq;
using System.Threading.Tasks;
using StitchSuApi.ContentDownloader.Services;
using StitchSuApi.Domain.Models;
using StitchSuApi.Domain.Repositories;

namespace StitchSuApi.Repositories
{
    public class KitPricesRepository: IKitPricesRepository
    {
        private readonly IWishlistKitsService _wishlistKitsService;

        public KitPricesRepository(IWishlistKitsService wishlistKitsService)
        {
            _wishlistKitsService = wishlistKitsService;
        }

        public async Task<KitPricesReport> GetKitPricesAsync()
        {
            var kits = await _wishlistKitsService.GetKitsAsync();
            return new KitPricesReport
            {
                Kits = kits
                    .Select(x => new Kit {
                        Title = x.Title,
                        Manufacturer = x.Manufacturer,
                        PreviewUrl = x.PreviewUrl,
                        Shops = x.Shops
                            .Select(
                                s => new Shop {
                                    Name = s.Name,
                                    Price = s.Price
                                })
                            .ToArray()
                    })
                    .ToArray(),
                ShopNames = kits
                    .SelectMany(x => x.Shops)
                    .Select(x => x.Name)
                    .Distinct()
                    .ToArray()
            };
        }
    }
}
