using System.Threading.Tasks;
using StitchSuApi.Domain.Models;

namespace StitchSuApi.Domain.Repositories
{
    public interface IKitPricesRepository
    {
        Task<KitPricesReport> GetKitPricesAsync();
    }
}
