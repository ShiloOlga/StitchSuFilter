using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public PatternPrice PriceInfo { get; private set; }
        public PatternDistributionStatus Status { get; private set; }

        private WishlistKitModel() { }

        public static WishlistKitModel Parse(IElement root, Uri uri)
        {
            var model = new WishlistKitModel();
            var id = GetId(root, uri);
            ItemImage image = GetImageInfo(root, uri);
            Manufacturer author = GetManufacturerInfo(root, id.ToString());
            Description patternInfo = GetDescriptionInfo(root);
            //PatternPrice patternPrice = GetPriceInfo(root);
            //PatternDistributionStatus status = GetStatus(root, patternPrice);
            //
            model.KitId = id;
            model.Image = image;
            model.Manufacturer = author;
            model.Info = patternInfo;
            //model.PriceInfo = patternPrice;
            //model.Status = status;
            return model;
        }

        public string GetColor()
        {
            switch (Status)
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
            }
        }

        public string Comment()
        {

            switch (Status)
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
            }
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
                if (lines.Length > 1)
                {
                    description.Title = lines[0];
                }
                if (lines.Length >= 4)
                {
                    description.Size = lines[1];
                    description.Fabric = lines[2];
                    description.AdditionalInfo = lines[3];
                }
            }

            return description;
        }

        private static PatternPrice GetPriceInfo(IElement root)
        {
            // Price
            var patternPrice = new PatternPrice();
            var priceContainer = root.QuerySelector("div.cart_button");
            if (priceContainer != null)
            {
                decimal price;
                decimal discountPrice = 0;
                var discountContainer = priceContainer.QuerySelector("span.action");
                if (discountContainer != null)
                {
                    decimal.TryParse(discountContainer.TextContent, out price);
                    decimal.TryParse(priceContainer.QuerySelector("span.pat_cost")?.TextContent, out discountPrice);
                }
                else
                {
                    decimal.TryParse(priceContainer.QuerySelector("span.pat_cost")?.TextContent, out price);
                }
                patternPrice.Price = price;
                patternPrice.DiscountPrice = discountPrice;
            }
            return patternPrice;
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
            sb.Append($"{KitId} - {Info.Title}. Status - {Status}");
            if (PriceInfo.HasPrice)
            {
                sb.Append($" ({PriceInfo.Price}");
                if (PriceInfo.HasDiscount)
                {
                    sb.Append($" , discount {PriceInfo.DiscountPercent}");
                }
                sb.Append(")");
            }
            return sb.ToString();
        }
    }
}
