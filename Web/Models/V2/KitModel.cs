using Web.Data;

namespace Web.Models.V2
{
    public enum KitType
    {
        ManufacturerKit,
        DesignerPattern
    }

    public class KitModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Manufacturer { get; set; }
        public string Item { get; set; }
        public Size Size { get; set; }
        public string ImageUrl { get; set; }
        public KitType KitType { get; set; }
        public bool HasXSD { get; set; }
    }
}
