using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.Context;
using Web.Data.Repositories;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICrossStitchKitsRepository _kitsRepository;
        private Random _random = new Random();

        public HomeController(ICrossStitchKitsRepository kitsRepository)
        {
            _kitsRepository = kitsRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sets = (await _kitsRepository.All())?.OrderBy(x => _random.Next()) ?? Enumerable.Empty<KitModel>();
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
