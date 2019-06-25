using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data.Repositories;
using Web.Models.CrossStitch;
using Web.Models.CrossStitch.Pattern;

namespace Web.Controllers
{
    public class CrossStitchController : Controller
    {
        private const string CookieKey = "PatternId";
        private readonly ICrossStitchRepository _crossStitchRepository;

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
            var model = BuildViewModel(items, "Updates", filter);
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
            var items = sourceItems.ToArray();
            var authors = CollectAuthors(items);
            var statuses = CollectStatuses(items);
            if (filter != null && !filter.IsEmpty)
            {
                var filterByAuthor = filter.Author != Filter.All;
                var filterByStatus = Enum.TryParse(typeof(PatternDistributionStatus), filter.Status, out var statusObject);
                if (filterByAuthor)
                {
                    items = items.Where(i => i.Author.Name == filter.Author).ToArray();
                    //change status list by author
                    statuses = CollectStatuses(items);
                    if (filterByStatus)
                    {
                        // filter by status if it exists for selected author
                        var status = (PatternDistributionStatus)statusObject;
                        if (items.Any(i => i.Status == status))
                        {
                            items = items.Where(i => i.Status == status).ToArray();
                        }
                    }
                }
                if (filterByStatus && !filterByAuthor)
                {
                    var status = (PatternDistributionStatus)statusObject;
                    items = items.Where(i => i.Status == status).ToArray();
                    //change authors list by status
                    authors = CollectAuthors(items);
                }
            }

            var model = new CrossStitchViewModel
            {
                MethodName = methodName,
                Items = items,
                Authors = authors.Select(x => new SelectListItem(x, x)),
                Author = filter?.Author,
                Status = filter?.Status,
                Statuses = statuses.Select(x => new SelectListItem(x, x))
            };
            return model;
        }

        private IEnumerable<string> CollectAuthors(IEnumerable<CrossStitchPatternModel> items)
        {
            var authors = items.Select(m => m.Author.Name).Distinct().OrderBy(m => m).ToList();
            authors.Insert(0, Filter.All);
            return authors;
        }

        private IEnumerable<string> CollectStatuses(IEnumerable<CrossStitchPatternModel> items)
        {
            var statuses = items.Select(m => m.Status.ToString()).Distinct().ToList();
            statuses.Insert(0, Filter.All);
            return statuses;
        }
    }
}