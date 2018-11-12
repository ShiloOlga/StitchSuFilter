using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models.CrossStitch;
using Web.Utils.CrossStitch;

namespace Web.Controllers
{
    public class CrossStitchController : Controller
    {
        private const string CookieKey = "PatternId";
        private ICrossStitchRepository _crossStitchRepository;

        public CrossStitchController(ICrossStitchRepository crossStitchRepository)
        {
            _crossStitchRepository = crossStitchRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Updates()
        {
            HttpContext.Request.Cookies.TryGetValue(CookieKey, out var lastSavedId);
            //if (models.Length > 0)
            //{

            //    //HttpContext.Response.Cookies.Append(CookieKey, models.OrderByDescending(m => m.PatternId.Id).First().PatternId.ToString());
            //}
            var models = await _crossStitchRepository.GetUpdates(lastSavedId);
            return View(models);
        }

        public async Task<IActionResult> Wishlist([FromForm] Filter filter = null)
        {
            var models = await _crossStitchRepository.GetWishlist();
            ViewBag.Authors = new SelectList(models.Select(m => m.Author.Name).Distinct().OrderBy(m => m));
            ViewBag.Statuses = new SelectList(models.Select(m => m.Status).Distinct());
            if (filter != null && !filter.IsEmpty)
            {
                models = models.Where(i => i.Author.Name == filter.Author && i.Status == filter.Status);
            }
            return View(models);
        }
    }
}