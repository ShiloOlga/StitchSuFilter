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
        private HtmlParser _domParser;

        public UpdatesDownloader()
        {
            _domParser = new HtmlParser();
        }

        public async Task<CrossStitchPageContent> Parse(string lastSavedId)
        {
            var hasSavedId = int.TryParse(lastSavedId, out var lastId);
            var models = new List<CrossStitchPatternModel>();
            var currentPageNum = 1;
            var hasMorePages = hasSavedId;
            do
            {
                var uri = new Uri($"https://www.stitch.su/patterns?favch=4&lim={ItemsPerPage}&page={currentPageNum}");
                var pageModels = await DownloadPatters(uri);
                if (hasSavedId)
                {
                    var minIdOnPage = pageModels.Min(m => m.PatternId.Id);
                    hasMorePages = minIdOnPage > lastId;
                    currentPageNum++;
                }
                models.AddRange(pageModels);
            }
            while (hasMorePages);
            var pageContent = new CrossStitchPageContent()
            {
                Patterns = models
            };
            return pageContent;
        }

        private async Task<IEnumerable<CrossStitchPatternModel>> DownloadPatters(Uri uri)
        {
            var content = await Download(uri);
            using (var document = await _domParser.ParseAsync(content))
            {
                return ParsePatterns(uri, document).ToList();
            }
        }

        private static IEnumerable<CrossStitchPatternModel> ParsePatterns(Uri uri, AngleSharp.Dom.Html.IHtmlDocument document)
        {
            var divNodes = document.All.Where(item => item.LocalName == "div").Where(item => item.ClassName != null && item.ClassName == "set");
            return divNodes.Select(item => CrossStitchPatternModel.Parse(item, uri));
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
