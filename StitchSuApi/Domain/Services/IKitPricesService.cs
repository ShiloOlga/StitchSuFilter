using System;
using System.Threading.Tasks;
using StitchSuApi.Domain.Models;

namespace StitchSuApi.Domain.Services
{
    public interface IKitPricesService
    {
        Task<KitPricesReport> GetKitPricesAsync();
    }
}
