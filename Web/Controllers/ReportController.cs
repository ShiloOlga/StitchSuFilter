using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Repositories;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class ReportController : Controller
    {
        private readonly ICrossStitchRepository _repository;
        private readonly ICrossStitchPatternsRepository _patternsRepository;

        public ReportController(ICrossStitchRepository repository, ICrossStitchPatternsRepository patternsRepository)
        {
            _repository = repository;
            _patternsRepository = patternsRepository;
        }

        public async Task<IActionResult> KitPrices()
        {
            var items = await _repository.GetWishlistKits();
            return View(KitPricesReportViewModel.Build(items));
        }

        public async Task<IActionResult> FabricReport()
        {
            var items = _patternsRepository.Patterns;
            return View(PatternFabricOptionsReportViewModel.Build(items));
        }

    }
}
