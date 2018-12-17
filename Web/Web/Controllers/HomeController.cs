using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICrossStitchKitsRepository _kitsRepository;

        public HomeController(ICrossStitchKitsRepository kitsRepository)
        {
            _kitsRepository = kitsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sets = (await _kitsRepository.All()) ?? Enumerable.Empty<Kit>();
            return View(sets);
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
                var dto = new Kit
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

        [Route("api/Setup")]
        public async Task<IActionResult> Setup()
        {
            if (_kitsRepository.IsEmpty)
            {
                await _kitsRepository.Add(new Kit
                {
                    Item = "1140", KitType = KitType.ManufacturerKit, Manufacturer = "Riolis",
                    Title = "Русская усадьба. Чай под яблоней", Size = "30х40",
                    ImageUrl = "http://www.riolis.ru/zoom/photos/2177.jpg"
                });
                await _kitsRepository.Add(new Kit
                {
                    Item = "807",
                    KitType = KitType.ManufacturerKit,
                    Manufacturer = "Riolis",
                    Title = "Одуванчики",
                    Size = "30х21",
                    ImageUrl = "http://www.riolis.ru/zoom/photos/1038.jpg"
                });
            }
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
