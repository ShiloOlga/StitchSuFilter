namespace Web.Models.CrossStitch
{
    public class PatternPrice
    {
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }

        public bool HasDiscount => HasPrice && DiscountPrice > 0;
        public bool HasPrice => Price > 0;

        public string DiscountPercent
        {
            get
            {
                if (Price > 0 && DiscountPrice > 0)
                {
                    var discount = (Price - DiscountPrice) / Price;
                    return $"{discount:#0.##%}";
                }
                return string.Empty;
            }
        }

        public string AsHtmlString()
        {
            if (!HasPrice)
                return "-";
            if (HasDiscount)
                return $"<s>{Price}</s><br>{DiscountPrice}";
            return Price.ToString();
        }
    }
}
