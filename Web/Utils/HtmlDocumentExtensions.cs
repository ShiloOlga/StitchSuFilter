using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Web.Models.CrossStitch;

namespace Web.Utils
{
    public static class HtmlDocumentExtensions
    {
        public static IEnumerable<CrossStitchPatternModel> SelectPatterns(this IHtmlDocument document, Uri uri)
        {
            var divNodes = document.All.Where(item => item.LocalName == "div").Where(i => i.Id != null && Regex.IsMatch(i.Id, @"^set_\d+$|fav_12_\d+$"));
            return divNodes.Select(item => CrossStitchPatternModel.Parse(item, uri));
        }

        public static IEnumerable<WishlistKitModel> SelectKits(this IHtmlDocument document, Uri uri)
        {
            var divNodes = document.All.Where(item => item.LocalName == "div").Where(i => i.Id != null && Regex.IsMatch(i.Id, @"^fav_1_\d+$"));
            return divNodes.Select(item => WishlistKitModel.Parse(item, uri));
        }

        public static int CalculatePagesCount(this IHtmlDocument document)
        {
            var totalPages = 1;
            var paginator = document.QuerySelector("div.paginator");
            var node = paginator.QuerySelectorAll("a")
                .Where(el => !string.IsNullOrEmpty(el.TextContent))
                .LastOrDefault();
                if (node != null && int.TryParse(node.TextContent, out var value))
                {
                    totalPages = value;
                }
            return totalPages;
        }
    }
}
