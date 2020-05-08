using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models.CrossStitch;

namespace Web.Utils.CrossStitch
{
    public class WishlistDownloader : ContentDownloader
    {
        public async Task<CrossStitchPageContent> Parse()
        {
            var patternModels = new List<CrossStitchPatternModel>();
            var kitModels = new List<WishlistKitModel>();
            var currentPageNum = 1;
            int? totalPages = null;
            do
            {
                var uri = new Uri($"https://www.stitch.su/users/wishlist/Lamya/page{currentPageNum}");
                using (var content = await DownloadContent(uri))
                {
                    totalPages ??= content.CalculatePagesCount();
                    patternModels.AddRange(content.SelectPatterns(uri));
                    kitModels.AddRange(content.SelectKits(uri));
                }
            } while (++currentPageNum <= totalPages.Value);
            var pageContent = new CrossStitchPageContent
            {
                Patterns = patternModels,
                Kits = kitModels
            };
            return pageContent;
        }
    }
}
