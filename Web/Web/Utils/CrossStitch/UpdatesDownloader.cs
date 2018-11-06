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
        public async Task<CrossStitchPageContent> Parse(string lastSavedId)
        {
            var hasSavedId = int.TryParse(lastSavedId, out var lastId);
            var models = new List<CrossStitchPatternModel>();
            var currentPageNum = 1;
            var hasMorePages = hasSavedId;
            do
            {
                var uri = new Uri($"https://www.stitch.su/patterns?favch=4&lim={ItemsPerPage}&page={currentPageNum}");
                using (var content = await DownloadContent(uri))
                {
                    var pageModels = content.SelectPatterns(uri);
                    if (hasSavedId)
                    {
                        var minIdOnPage = pageModels.Min(m => m.PatternId.Id);
                        hasMorePages = minIdOnPage > lastId;
                        currentPageNum++;
                    }
                    models.AddRange(pageModels);
                }
            }
            while (hasMorePages);
            var pageContent = new CrossStitchPageContent()
            {
                Patterns = models
            };
            return pageContent;
        }


        public async Task<CrossStitchPageContent> Parse(int pageId)
        {
            var models = new List<CrossStitchPatternModel>();
            var uri = new Uri($"https://www.stitch.su/patterns?favch=4&lim={ItemsPerPage}&page={pageId}");
            using (var content = await DownloadContent(uri))
            {
                var pageModels = content.SelectPatterns(uri);
                models.AddRange(pageModels);
            }
            var pageContent = new CrossStitchPageContent()
            {
                Patterns = models
            };
            return pageContent;
        }
    }
}
