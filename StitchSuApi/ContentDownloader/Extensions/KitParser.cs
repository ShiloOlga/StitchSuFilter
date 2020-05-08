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
                .ParseTitle(e);
            //var image = GetImageInfo(root, uri);
            //var author = GetManufacturerInfo(root, id.ToString());
            //var descriptionInfo = GetDescriptionInfo(root);
            //var availableShops = GetPriceInfo(root, uri);
            //var tags = GetTags(root);
            ////PatternPrice patternPrice = GetPriceInfo(root);
            ////PatternDistributionStatus status = GetStatus(root, patternPrice);
            ////
            //kit.KitId = id;
            //kit.Image = image;
            //kit.Manufacturer = author;
            //kit.Info = descriptionInfo;
            //kit.Tags = tags;
            //kit.AvailableShops = availableShops;
            ////model.PriceInfo = patternPrice;
            ////model.Status = status;
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
    }
}
