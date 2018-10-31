using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public sealed class HtmlProcessingUtility
    {
        public static void ReplaceAllRelativeLinks(IElement document, Uri sourceUri)
        {
            ReplaceAllRelativeLinksInternal(document, sourceUri);
        }

        private static void ReplaceAllRelativeLinksInternal(IElement root, Uri sourceUri)
        {
            if (root == null)
                return;

            //if (root.HasChildNodes)
            //{
            //    foreach (var child in root.Children)
            //    {
            //        ReplaceAllRelativeLinksInternal(child, sourceUri);
            //    }
            //}

            //if (root.Attributes.Any())
            //{
            var matchCollection = Regex.Matches(root.OuterHtml, "(?:url\\(|<(?:link|script|img|div|a)[^>]+(?:src|href)\\s*=\\s*)(?!['\"]?(?:data|http|//))['\"]?([^'\"\\)#\\s>]+)");
            if (matchCollection.Count > 0)
            {
                var matchSet = new HashSet<string>();
                var replacedString = root.OuterHtml;
                foreach (Match match in matchCollection)
                {
                    if (match.Groups.Count > 0)
                    {
                        var relativeUrl = match.Groups[match.Groups.Count - 1].Value;
                        matchSet.Add(relativeUrl);
                    }
                }
                foreach (var relativeUrl in matchSet)
                {
                    replacedString = replacedString.Replace(relativeUrl, BuildAbsoluteUri(sourceUri, relativeUrl));
                }
                root.OuterHtml = replacedString;
                // }
            }
        }

        public static string BuildAbsoluteUri(Uri sourceUri, string relativeUrl)
        {
            var builder = new UriBuilder(sourceUri.Scheme, sourceUri.Host);
            if (relativeUrl != null)
            {
                var trimmedRelativeUrl = relativeUrl.Trim();
                builder.Path = trimmedRelativeUrl.Length > 0 ? trimmedRelativeUrl : builder.Path;
            }
            return builder.Uri.AbsoluteUri;
        }
    }
}
