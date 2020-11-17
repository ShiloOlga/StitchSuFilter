using StitchSuApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using StitchSuApi.Domain.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace StitchSuApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
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
