using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Web.Utils.CrossStitch;

namespace Web.Controllers
{
    public class CrossStitchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Updates()
        {
            var updatesDownloader = new UpdatesDownloader();
            var models = (await updatesDownloader.Parse()).ToArray();
            if (models.Length > 0)
            {
                if (HttpContext.Request.Cookies.ContainsKey("PatternId"))
                {

                }
                HttpContext.Response.Cookies.Append("PatternId", models.OrderByDescending(m => m.PatternId.Id).First().PatternId.ToString());
            }
            return View(models);
        }

        public string Personal()
        {
            return "Personal";
        }
    }
}