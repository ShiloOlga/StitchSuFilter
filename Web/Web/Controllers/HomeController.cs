using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Repositories;
using Web.Models;
using Web.Models.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public const int ItemsPerPage = 25;
        private readonly ICrossStitchKitsRepository _kitsRepository;
        private readonly Random _random = new Random();

        public HomeController(ICrossStitchKitsRepository kitsRepository)
        {
            _kitsRepository = kitsRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var patterns = (await _kitsRepository.AllPatterns()).ToList();
            var viewModel = new KitSummaryViewModel
            {
                KitItems = patterns
                    .Skip((page - 1) * ItemsPerPage)
                    .Take(ItemsPerPage)
                    .OrderBy(x => _random.Next()),
                PagingInfo = new PagingModel
                {
                    CurrentPage = page,
                    PageSize = ItemsPerPage,
                    TotalCount = patterns.Count
                }
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Kits(int page = 1)
        {
            var kits = (await _kitsRepository.AllKits()).ToList();
            var viewModel = new KitSummaryViewModel
            {
                KitItems = kits
                    .Skip((page - 1) * ItemsPerPage)
                    .Take(ItemsPerPage)
                    .OrderBy(x => _random.Next()),
                PagingInfo = new PagingModel
                {
                    CurrentPage = page,
                    PageSize = ItemsPerPage,
                    TotalCount = kits.Count
                }
            };
            return View("Index", viewModel);
        }

        public IActionResult Create()
        {
            return View(nameof(Edit), new KitViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Edit(KitViewModel kit)
        {
            if (ModelState.IsValid)
            {
                var dto = new KitModel
                {
                    Id = kit.Id,
                    Title = kit.Title,
                    ImageUrl = kit.ImageUrl,
                    Manufacturer = kit.Manufacturer,
                    KitType = kit.KitType,
                    Size = kit.Size,
                    Item = kit.Item,
                };
                await _kitsRepository.Add(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(kit);
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Route("api/Execute")]
        public async Task<IActionResult> Execute()
        {
            await _kitsRepository.Execute();
            return StatusCode(200);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
