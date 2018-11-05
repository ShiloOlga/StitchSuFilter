using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Web.Utils.CrossStitch;

namespace Web.Controllers
{
    public class CrossStitchController : Controller
    {
        private const string CookieKey = "PatternId";

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Updates()
        {
            HttpContext.Request.Cookies.TryGetValue(CookieKey, out var lastSavedId);
            var updatesDownloader = new UpdatesDownloader();
            var pageContent = await updatesDownloader.Parse(lastSavedId);
            var models = pageContent.Patterns.ToArray();
            if (models.Length > 0)
            {

                //HttpContext.Response.Cookies.Append(CookieKey, models.OrderByDescending(m => m.PatternId.Id).First().PatternId.ToString());
            }
            return View(models);
        }

        public async Task<IActionResult> Wishlist()
        {
            var wishlistDownloader = new WishlistDownloader();
            var pageContent = await wishlistDownloader.Parse();
            var models = pageContent.Patterns.ToArray();
            return View(models);
        }
    }
}