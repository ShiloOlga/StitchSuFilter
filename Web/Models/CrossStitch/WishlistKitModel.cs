using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using Web.Models.CrossStitch.Kit;
using Web.Models.CrossStitch.Pattern;
using Web.Utils;

namespace Web.Models.CrossStitch
{
    public class WishlistKitModel : IWishlistItemModel
    {
        public KitId KitId { get; private set; }
        public Manufacturer Manufacturer { get; private set; }
        public ItemImage Image { get; private set; }
        public Description Info { get; private set; }
        public IEnumerable<Tag> Tags { get; private set; }
        public IEnumerable<ShopInfo> AvailableShops { get; private set; }
        //public PatternPrice PriceInfo { get; private set; }
        //public PatternDistributionStatus Status { get; private set; }

        private WishlistKitModel() { }

        public static WishlistKitModel Parse(IElement root, Uri uri)
        {
            var model = new WishlistKitModel();
            var id = GetId(root, uri);
            var image = GetImageInfo(root, uri);
            var author = GetManufacturerInfo(root, id.ToString());
            var descriptionInfo = GetDescriptionInfo(root);
            var availableShops = GetPriceInfo(root, uri);
            var tags = GetTags(root);
            //PatternPrice patternPrice = GetPriceInfo(root);
            //PatternDistributionStatus status = GetStatus(root, patternPrice);
            //
            model.KitId = id;
            model.Image = image;
            model.Manufacturer = author;
            model.Info = descriptionInfo;
            model.Tags = tags;
            model.AvailableShops = availableShops;
            //model.PriceInfo = patternPrice;
            //model.Status = status;
            return model;
        }

        public string GetColor()
        {
            return "#FFFFFF";
            /*switch (Status)
            {
                case PatternDistributionStatus.AvailableToBuy:
                    return "#FAFFE3";
                case PatternDistributionStatus.OnSale:
                    return "#FFDEC3";
                case PatternDistributionStatus.FreeToDownload:
                    return "#F1E6FF";
                case PatternDistributionStatus.AuthorRequestOnly:
                    return "#C3EEF2";
                case PatternDistributionStatus.Undefined:
                default:
                    return "#FFFFFF";
            }*/
        }

        public string Comment()
        {
            return "    ";
            /*switch (Status)
            {
                case PatternDistributionStatus.OnSale:
                    return PriceInfo.DiscountPercent;
                case PatternDistributionStatus.FreeToDownload:
                    return "Free";
                case PatternDistributionStatus.AuthorRequestOnly:
                case PatternDistributionStatus.AvailableToBuy:
                case PatternDistributionStatus.Undefined:
                default:
                    return "    ";
            }*/
        }

        private static KitId GetId(IElement root, Uri uri)
        {
            // Id
            var id = new KitId();

            var container = root.QuerySelector("div.set__title");
            if (container != null)
            {
                var name = container.TextContent;
                if (!string.IsNullOrEmpty(name))
                {
                    name = name.Substring(name.LastIndexOf(' ')).Trim(' ', '#', '\xA0');
                }
                id.Id = name;
            }
            var linkContainer = root.QuerySelector("a.set__link");
            if (linkContainer != null)
            {
                id.Link = HtmlProcessingUtility.BuildAbsoluteUri(uri, linkContainer.GetAttribute("href"));
            }
            return id;
        }

        private static ItemImage GetImageInfo(IElement root, Uri uri)
        {
            //Image
            var image = new ItemImage();
            var imageContainer = root.QuerySelector("div.set__pic")?.QuerySelector("a");
            if (imageContainer != null)
            {
                image.Description = imageContainer.GetAttribute("title");
                image.FullImageUrl = HtmlProcessingUtility.BuildAbsoluteUri(uri, imageContainer.GetAttribute("href"));
                image.PreviewImageUrl = HtmlProcessingUtility.BuildAbsoluteUri(uri, imageContainer.QuerySelector("img")?.GetAttribute("src"));
            }

            return image;
        }

        private static Manufacturer GetManufacturerInfo(IElement root, string id)
        {
            var manufacturer = new Manufacturer();
            var container = root.QuerySelector("div.set__title");
            if (container != null)
            {
                var name = container.TextContent;
                if (!string.IsNullOrEmpty(name))
                {
                    name = name.Replace(id, string.Empty).Trim(' ', '#', '\xA0');
                }
                manufacturer.Name = name;
            }

            return manufacturer;
        }

        private static Description GetDescriptionInfo(IElement root)
        {
            var description = new Description();
            var infoContainer = root.QuerySelector("div.set__info") as IHtmlDivElement;
            if (infoContainer != null && !string.IsNullOrEmpty(infoContainer.InnerText))
            {
                var lines = infoContainer.InnerText.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length >= 4)
                {
                    description.Title = lines.Take(lines.Length - 3).Aggregate((x, y) => x + ", " + y);
                    description.Size = lines[lines.Length - 3];
                    description.Fabric = lines[lines.Length - 2];
                    description.AdditionalInfo = lines[lines.Length - 1];
                }
                else if (lines.Length == 3)
                {
                    description.Size = lines[lines.Length - 3];
                    description.Fabric = lines[lines.Length - 2];
                    description.AdditionalInfo = lines[lines.Length - 1];
                    description.Title = "-";
                }
                else
                {
                    description.Title = infoContainer.InnerText;
                }
            }

            return description;
        }

        private static IEnumerable<Tag> GetTags(IElement root)
        {
            var tags = new List<Tag>();
            var tagLinks = root.QuerySelector("div.tags")?.QuerySelectorAll("a");
            if (tagLinks?.Length > 0)
            {
                foreach (var t in tagLinks)
                {
                    tags.Add(new Tag
                    {
                        Name = t.TextContent,
                        Link = t.GetAttribute("href")
                    });
                }
            }

            return tags;
        }

        private static IEnumerable<ShopInfo> GetPriceInfo(IElement root, Uri uri)
        {
            // Price
            var container = root.QuerySelectorAll("table tr");
            var result = new List<ShopInfo>(container.Length);
            foreach (var item in container)
            {
                var dto = new ShopInfo();
                if (item.HasChildNodes && item.ChildElementCount == 2)
                {
                    var shopInfo = item.Children[0];
                    if (item.QuerySelector("a") is IHtmlAnchorElement link)
                    {
                        dto.Name = link.Text;
                        dto.Link = HtmlProcessingUtility.BuildAbsoluteUri(uri, link.PathName + link.Search);
                    }
                    else
                    {
                        dto.Name = shopInfo.TextContent;
                    }
                    dto.Price = decimal.Parse(Regex.Match(item.Children[1].TextContent, @"\d+").Value);
                    result.Add(dto);
                }
                else if (item.QuerySelector("td.nomags") == null)
                {
                    throw new Exception("Invalid kit shop line format");
                }
            }
            return result.OrderBy(x => x.Price);
        }

        private static PatternDistributionStatus GetStatus(IElement root, PatternPrice patternPrice)
        {
            // Status
            var status = PatternDistributionStatus.Undefined;
            if (patternPrice.HasDiscount)
            {
                status = PatternDistributionStatus.OnSale;
            }
            else if (patternPrice.HasPrice)
            {
                status = PatternDistributionStatus.AvailableToBuy;
            }
            else
            {
                var comment = root.QuerySelector("div.free_text");
                if (comment != null)
                {
                    status = comment.TextContent != null && comment.TextContent.ToLowerInvariant().Contains("бесплатно")
                        ? PatternDistributionStatus.FreeToDownload
                        : PatternDistributionStatus.AuthorRequestOnly;
                }
            }

            return status;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append($"{KitId} - {Info.Title}.");
            return sb.ToString();
        }
    }
}
