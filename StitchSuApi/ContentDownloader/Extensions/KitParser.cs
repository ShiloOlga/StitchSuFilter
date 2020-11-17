using System;
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using StitchSuApi.ContentDownloader.Models;

namespace StitchSuApi.ContentDownloader.Extensions
{
    public static class KitParser
    {
        private static readonly char[] _emptyChars = {' ', '#', '\xA0'};

        public static Kit FromElement(this IElement e)
        {
            var kit = new Kit()
                .ParseHeader(e)
                .ParseImage(e)
                .ParseTitle(e)
                .ParseShops(e);
            return kit;
        }

        private static Kit ParseHeader(this Kit kit, IElement root)
        {
            var container = root.QuerySelector("div.set__title");
            if (container != null)
            {
                var content = container.TextContent;
                if (!string.IsNullOrEmpty(content))
                {
                    var kitIdIndex = content.LastIndexOf(' ');
                    kit.Id = content.Substring(kitIdIndex).Trim(_emptyChars);
                    kit.Manufacturer = content.Substring(0, kitIdIndex).Trim(_emptyChars);
                }
            }
            var linkContainer = root.QuerySelector("a.set__link");
            if (linkContainer != null)
            {
                kit.Href = linkContainer.GetAttribute("href");
            }
            return kit;
        }

        private static Kit ParseImage(this Kit kit, IElement root)
        {
            var imageContainer = root.QuerySelector("div.set__pic")?.QuerySelector("a");
            kit.PreviewUrl = imageContainer?.QuerySelector("img")?.GetAttribute("src");
            return kit;
        }

        private static Kit ParseTitle(this Kit kit, IElement root)
        {
            var infoContainer = root.QuerySelector("div.set__info") as IHtmlDivElement;
            var content = infoContainer?.TextContent;
            if (!string.IsNullOrEmpty(content))
            {
                var hasBrackets = false;
                var index = 0;
                while (index < content.Length)
                {
                    if (content[index] == '(')
                    {
                        hasBrackets = true;
                    }
                    if (content[index] == ')')
                    {
                        hasBrackets = false;
                    }
                    if (content[index] == ',' && !hasBrackets)
                    {
                        break;
                    }
                    index++;
                }
                kit.Title = content.Substring(0, index);
            }

            return kit;
        }

        private static Kit ParseShops(this Kit kit, IElement root)
        {
            var container = root.QuerySelectorAll("table tr");
            var shops = new List<Shop>(container.Length);
            foreach (var item in container)
            {
                if (item.HasChildNodes && item.ChildElementCount == 2)
                {
                    var shop = new Shop();
                    shop.Name = item.Children[0].TextContent;
                    shop.Price = decimal.Parse(System.Text.RegularExpressions.Regex.Match(item.Children[1].TextContent, @"\d+").Value);
                    shops.Add(shop);
                }
                else if (item.QuerySelector("td.nomags") == null)
                {
                    throw new Exception("Invalid kit shop line format");
                }
            }
            kit.Shops = shops
                .OrderBy(x => x.Price)
                .ToArray();
            return kit;
        }
    }
}
