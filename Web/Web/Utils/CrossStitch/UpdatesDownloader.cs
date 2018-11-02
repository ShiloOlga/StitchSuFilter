using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.CrossStitch;

namespace Web.Utils.CrossStitch
{
    public class UpdatesDownloader : ContentDownloader
    {
        public async Task<IEnumerable<StitchSuPatternModel>> Parse()
        {
            var uri = new Uri("https://www.stitch.su/patterns?favch=4&page=3&lim=50");
            var models = new List<StitchSuPatternModel>();
            var content = await Download(uri);
            var domParser = new HtmlParser();
            using (var document = await domParser.ParseAsync(content))
            {
                var divNodes = document.All.Where(item => item.LocalName == "div").Where(item => item.ClassName != null && item.ClassName == "set");
                models.AddRange(divNodes.Select(item => StitchSuPatternModel.Parse(item, uri)));
            }
            return models;
        }
    }
}
