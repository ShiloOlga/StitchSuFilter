using StitchSuApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using StitchSuApi.Domain.Services;
using System.Threading.Tasks;

namespace StitchSuApi.Controllers
{
    [Route("api/[controller]")]
    public class KitPricesReportController : Controller
    {
        private readonly IKitPricesService _kitPricesService;

        public KitPricesReportController(IKitPricesService kitPricesService)
        {
            _kitPricesService = kitPricesService;
        }

        // GET: api/values
        [HttpGet]
        public async Task<KitPricesReport> GetAsync()
        {
            var report = await _kitPricesService.GetKitPricesAsync();
            return report;
        }
    }
}
