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
            var divNodes = document.All.Where(item => item.LocalName == "div").Where(i => i.Id != null && Regex.IsMatch(i.Id, @"^(\bset|\bfav)_[_\d]+$"));
            return divNodes.Select(item => CrossStitchPatternModel.Parse(item, uri));
        }
        public static int CalculatePagesCount(this IHtmlDocument document)
        {
            var totalPages = 1;
            var paginator = document.QuerySelector("div.paginator");
            var aNodes = paginator.QuerySelectorAll("a").Where(el => !string.IsNullOrEmpty(el.TextContent));
            foreach (var node in aNodes)
            {
                if (int.TryParse(node.TextContent, out var value))
                {
                    totalPages = Math.Max(totalPages, value);
                }
            }
            return totalPages;
        }
    }
}
