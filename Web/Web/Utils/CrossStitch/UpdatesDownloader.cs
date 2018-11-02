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
        private const int ItemsPerPage = 50;

        public async Task<CrossStitchPageContent> Parse()
        {
            var uri = new Uri($"https://www.stitch.su/patterns?favch=4&page=3&lim={ItemsPerPage}");
            var content = await Download(uri);
            var domParser = new HtmlParser();
            using (var document = await domParser.ParseAsync(content))
            {
                var models = ParsePatterns(uri, document).ToList();
                var pageContent = new CrossStitchPageContent()
                {
                    Patterns = models,
                    PageCount = ParsePageNavigator(document).TotalCount
                };
                return pageContent;
            }
        }

        private static IEnumerable<StitchSuPatternModel> ParsePatterns(Uri uri, AngleSharp.Dom.Html.IHtmlDocument document)
        {
            var divNodes = document.All.Where(item => item.LocalName == "div").Where(item => item.ClassName != null && item.ClassName == "set");
            return divNodes.Select(item => StitchSuPatternModel.Parse(item, uri));
        }

        private static PagesInfo ParsePageNavigator(AngleSharp.Dom.Html.IHtmlDocument document)
        {
            var pagesInfo = new PagesInfo()
            {
                ItemsPerPage = ItemsPerPage
            };
            var totalPages = 1;
            var aNodes = document.QuerySelector("div.paginator").QuerySelectorAll("a").Where(el => !string.IsNullOrEmpty(el.TextContent));
            foreach (var node in aNodes)
            {
                if (int.TryParse(node.TextContent, out var value))
                {
                    totalPages = Math.Max(totalPages, value);
                }
            }
            pagesInfo.TotalCount = totalPages;
            return pagesInfo;
        }
    }
}
