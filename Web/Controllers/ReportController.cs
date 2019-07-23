using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Repositories;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly ICrossStitchRepository _repository;

        public ReportController(ICrossStitchRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> KitPrices()
        {
            var items = await _repository.GetWishlistKits();
            return View(KitPricesReportViewModel.Build(items));
        }

    }
}
