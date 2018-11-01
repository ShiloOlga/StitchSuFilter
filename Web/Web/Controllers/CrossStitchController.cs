using AngleSharp.Parser.Html;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Models.CrossStitch;

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
            var uri = new Uri("https://www.stitch.su/patterns?favch=4&page=3&lim=50");
            var models = new List<StitchSuPatternModel>();
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri.AbsoluteUri);
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var domParser = new HtmlParser();
                    using (var document = await domParser.ParseAsync(content))
                    {

                        var divNodes = document.All.Where(item => item.LocalName == "div").Where(item => item.ClassName != null && item.ClassName == "set");
                        models.AddRange(divNodes.Select(item => StitchSuPatternModel.Parse(item, uri)));
                    }
                }
            }
            return View(models);
        }

        public string Personal()
        {
            return "Personal";
        }
    }
}