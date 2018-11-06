using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
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
            //HttpContext.Request.Cookies.TryGetValue(CookieKey, out var lastSavedId);
            //var updatesDownloader = new UpdatesDownloader();
            //var pageContent = await updatesDownloader.Parse(lastSavedId);
            //var models = pageContent.Patterns.ToArray();
            //if (models.Length > 0)
            //{

            //    //HttpContext.Response.Cookies.Append(CookieKey, models.OrderByDescending(m => m.PatternId.Id).First().PatternId.ToString());
            //}
            var models = await _crossStitchRepository.GetUpdates();
            return View(models);
        }

        public async Task<IActionResult> Wishlist()
        {
            var models = await _crossStitchRepository.GetWishlist();
            return View(models);
        }
    }
}