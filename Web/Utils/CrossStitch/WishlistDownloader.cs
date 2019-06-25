using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models.CrossStitch;

namespace Web.Utils.CrossStitch
{
    public class WishlistDownloader : ContentDownloader
    {
        public async Task<CrossStitchPageContent> Parse()
        {
            var models = new List<CrossStitchPatternModel>();
            var currentPageNum = 1;
            var uri = new Uri($"https://www.stitch.su/users/wishlist/Lamya");
            using (var content = await DownloadContent(uri))
            {
                var pageModels = content.SelectPatterns(uri);
                var pages = content.CalculatePagesCount();
                models.AddRange(pageModels);
                for (var i = currentPageNum + 1; i <= pages; i++)
                {
                    var patternsFromPage = await SelectPatternsFromPage(i);
                    models.AddRange(patternsFromPage);
                }
            }
            var pageContent = new CrossStitchPageContent()
            {
                Patterns = models
            };
            return pageContent;
        }

        private async Task<IEnumerable<CrossStitchPatternModel>> SelectPatternsFromPage(int pageId)
        {
            var uri = new Uri($"https://www.stitch.su/users/wishlist/Lamya/page{pageId}");
            using (var content = await DownloadContent(uri))
            {
                var pageModels = content.SelectPatterns(uri);
                return pageModels.ToArray();
            }
        }
    }
}
