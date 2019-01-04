using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Data.Repositories;
using Web.Models;

namespace Web.Controllers
{
    public class AddFabricController : Controller
    {
        private readonly ICrossStitchKitsRepository _repository;

        public AddFabricController(ICrossStitchKitsRepository repository)
        {
            _repository = repository;
        }

        public async Task<ViewResult> Index()
        {

            var existingItems = await _repository.GetFabricItems();
            return View(new AddFabricViewModel { ExistingItems = existingItems });
        }
        [HttpGet]
        public async Task<ActionResult> GetColors(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var existingItems = await _repository.GetFabricItems();
                var colors = existingItems
                    .Where(fi => fi.Fabric.Name == name)
                    .OrderBy(fi => fi.ColorId.Length)
                    .ThenBy(fi => fi.ColorId)
                    .Select(fi => new SelectListItem($"{fi.ColorId} - {fi.ColorName}", fi.ColorId))
                    .ToList();
                return Json(colors);
            }
            return null;
        }
    }
}