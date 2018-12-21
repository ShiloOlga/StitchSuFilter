using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    public class PatternDetailsController : Controller
    {
        private readonly ICrossStitchKitsRepository _kitsRepository;

        public PatternDetailsController(ICrossStitchKitsRepository kitsRepository)
        {
            _kitsRepository = kitsRepository;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(string item)
        {
            var kit =  await _kitsRepository.GetByItem(item);
            var model = new PatternDetailsViewModel
            {
                Id = kit.Id,
                Title = kit.Title,
                ImageUrl = kit.ImageUrl,
                Manufacturer = kit.Manufacturer,
                KitType = kit.KitType,
                Size = kit.Size,
                Item = kit.Item,
            };
            return View(model ?? new PatternDetailsViewModel());
        }
    }
}
