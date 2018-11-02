using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using System;
using System.Text;
using Web.Utils;

namespace Web.Models.CrossStitch
{

    public class StitchSuPatternModel
    {
        public static StitchSuPatternModel Default = new StitchSuPatternModel();

        public PatternId PatternId { get; private set; }
        public PatternAuthor Author { get; private set; }
        public PatternImage Image { get; private set; }
        public PatternInfo Info { get; private set; }
        public PatternPrice PriceInfo { get; private set; }
        public PatternDistributionStatus Status { get; private set; }

        private StitchSuPatternModel() { }

        public static StitchSuPatternModel Parse(IElement root, Uri uri)
        {
            var model = new StitchSuPatternModel();
            var id = GetId(root, uri);
            PatternImage image = GetImageInfo(root, uri);
            PatternAuthor author = GetAuthorInfo(root, id.ToString());
            PatternInfo patternInfo = GetPatternDescriptionInfo(root);
            PatternPrice patternPrice = GetPriceInfo(root);
            PatternDistributionStatus status = GetStatus(root, patternPrice);
            //
            model.PatternId = id;
            model.Image = image;
            model.Author = author;
            model.Info = patternInfo;
            model.PriceInfo = patternPrice;
            model.Status = status;
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

        private static PatternId GetId(IElement root, Uri uri)
        {
            // Id
            var id = new PatternId();
            if (!string.IsNullOrEmpty(root.Id))
            {
                id.Id = int.Parse(root.Id.Replace("set_", string.Empty));
            }
            var linkContainer = root.QuerySelector("a.set__link");
            if (linkContainer != null)
            {
                id.PatternLink = HtmlProcessingUtility.BuildAbsoluteUri(uri, linkContainer.GetAttribute("href"));
            }
            return id;
        }

        private static PatternImage GetImageInfo(IElement root, Uri uri)
        {
            //Image
            var image = new PatternImage();
            var imageContainer = root.QuerySelector("div.set__pic")?.QuerySelector("a");
            if (imageContainer != null)
            {
                image.Description = imageContainer.GetAttribute("title");
                image.FullImageUrl = HtmlProcessingUtility.BuildAbsoluteUri(uri, imageContainer.GetAttribute("href"));
                image.PreviewImageUrl = HtmlProcessingUtility.BuildAbsoluteUri(uri, imageContainer.QuerySelector("img")?.GetAttribute("src"));
            }

            return image;
        }

        private static PatternAuthor GetAuthorInfo(IElement root, string id)
        {
            // Author
            var author = new PatternAuthor();
            var authorContainer = root.QuerySelector("div.set__title");
            if (authorContainer != null)
            {
                author.ProfileLink = authorContainer.QuerySelector("a")?.GetAttribute("href");
                var authorName = authorContainer.TextContent;
                if (!string.IsNullOrEmpty(authorName))
                {
                    authorName = authorName.Replace(id, string.Empty).Trim(' ', '#', '\xA0');
                }
                author.Name = authorName;
            }

            return author;
        }

        private static PatternInfo GetPatternDescriptionInfo(IElement root)
        {
            // Pattern info
            var patternInfo = new PatternInfo();
            var infoContainer = root.QuerySelector("div.set__info") as IHtmlDivElement;
            if (infoContainer != null && !string.IsNullOrEmpty(infoContainer.InnerText))
            {
                var lines = infoContainer.InnerText.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length >= 2)
                {
                    patternInfo.Title = lines[0];
                    patternInfo.Description = infoContainer.InnerText.Replace(patternInfo.Title, string.Empty).TrimStart('.', ' ', '\n');
                }
                if (lines.Length == 1)
                {
                    var singleLine = lines[0];
                    if (singleLine.ToLowerInvariant().Contains("размер"))
                    {
                        patternInfo.Description = singleLine;
                    }
                    else
                    {
                        patternInfo.Title = singleLine;
                    }
                }
            }

            return patternInfo;
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
            sb.Append($"{PatternId} - {Info.Title}. Status - {Status}");
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
