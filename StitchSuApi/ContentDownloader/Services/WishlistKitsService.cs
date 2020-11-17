using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using StitchSuApi.ContentDownloader.Extensions;
using StitchSuApi.ContentDownloader.Models;

namespace StitchSuApi.ContentDownloader.Services
{
    public interface IWishlistKitsService
    {
        Task<IEnumerable<Kit>> GetKitsAsync();
    }

    public class WishlistKitsService : IWishlistKitsService
    {
        public WishlistKitsService()
        {
        }

        public async Task<IEnumerable<Kit>> GetKitsAsync()
        {
            var currentPageNum = 1;
            int? totalPages = null;
            var kits = new List<Kit>();
            var parser = new HtmlParser();
            do
            {
                var uri = new Uri($"https://www.stitch.su/users/wishlist/Lamya/label=наборы/page{currentPageNum}");
                using (var content = await DownloadContent(uri, parser))
                {
                    totalPages = totalPages ?? CalculatePagesCount(content);
                    kits.AddRange(SelectKits(content, uri));
                }
            } while (++currentPageNum <= totalPages.Value);
            return kits;
        }

        protected async Task<IHtmlDocument> DownloadContent(Uri uri, HtmlParser parser)
        {
            var content = await Download(uri);
            return await parser.ParseDocumentAsync(content);
        }

        private async Task<string> Download(Uri uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri.AbsoluteUri);
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
            }
            return string.Empty;
        }

        public static int CalculatePagesCount(IHtmlDocument document)
        {
            var totalPages = 1;
            var paginator = document.QuerySelector("div.paginator");
            var node = paginator?.QuerySelectorAll("a")
                .Where(el => !string.IsNullOrEmpty(el.TextContent))
                .LastOrDefault();
            if (node != null && int.TryParse(node.TextContent, out var value))
            {
                totalPages = value;
            }
            return totalPages;
        }

        public static IEnumerable<Kit> SelectKits(IHtmlDocument document, Uri uri)
        {
            var divNodes = document.All.Where(item => item.LocalName == "div"
                && item.Id != null && Regex.IsMatch(item.Id, @"^fav_1_\d+$"));
            return divNodes.Select(node => node.FromElement());
        }
    }
}
