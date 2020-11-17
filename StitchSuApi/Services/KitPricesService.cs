using System.Threading.Tasks;
using StitchSuApi.Domain.Models;
using StitchSuApi.Domain.Repositories;
using StitchSuApi.Domain.Services;

namespace StitchSuApi.Services
{
    public class KitPricesService: IKitPricesService
    {
        private readonly IKitPricesRepository _kitPricesRepository;

        public KitPricesService(IKitPricesRepository kitPricesRepository)
        {
            _kitPricesRepository = kitPricesRepository;
        }

        public Task<KitPricesReport> GetKitPricesAsync()
        {
            return _kitPricesRepository.GetKitPricesAsync();
        }
    }
}
