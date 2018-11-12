using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models.CrossStitch;

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

        public async Task<IActionResult> Updates([FromForm] Filter filter = null)
        {
            HttpContext.Request.Cookies.TryGetValue(CookieKey, out var lastSavedId);
            //if (models.Length > 0)
            //{

            //    //HttpContext.Response.Cookies.Append(CookieKey, models.OrderByDescending(m => m.PatternId.Id).First().PatternId.ToString());
            //}
            var items = await _crossStitchRepository.GetUpdates(lastSavedId);
            var model = BuildViewModel(items, "Updates",  filter);
            return View(model);
        }

        public async Task<IActionResult> Wishlist([FromForm] Filter filter = null)
        {
            var items = await _crossStitchRepository.GetWishlist();
            var model = BuildViewModel(items, "Wishlist", filter);
            return View(model);
        }

        private CrossStitchViewModel BuildViewModel(IEnumerable<CrossStitchPatternModel> sourceItems, string methodName, Filter filter = null)
        {
            var items = sourceItems;
            var authors = items.Select(m => m.Author.Name).Distinct().OrderBy(m => m).ToList();
            authors.Insert(0, Filter.All);
            var statuses = items.Select(m => m.Status.ToString()).Distinct().ToList();
            statuses.Insert(0, Filter.All);
            var model = new CrossStitchViewModel
            {
                Authors = new SelectList(authors),
                Statuses = new SelectList(statuses),
                MethodName = methodName
            };
            if (filter != null && !filter.IsEmpty)
            {
                if (filter.Author != Filter.All)
                {
                    items = items.Where(i => i.Author.Name == filter.Author);
                }
                if (Enum.TryParse(typeof(PatternDistributionStatus), filter.Status, out var statusObject))
                {
                    var status = (PatternDistributionStatus)statusObject;
                    items = items.Where(i => i.Status == status);
                }
            }
            model.Items = items.ToArray();
            return model;
        }
    }
}