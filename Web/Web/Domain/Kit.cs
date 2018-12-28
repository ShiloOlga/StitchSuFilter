namespace Web.Domain
{
    public partial class Kit
    {
        public int Id { get; set; }
        public int? ManufacturerId { get; set; }
        public string Title { get; set; }
        public string Item { get; set; }
        public int? ThreadManufacturerId { get; set; }
        public int? FabricItemId { get; set; }
        public int ColorsCount { get; set; }
        public decimal WidthSm { get; set; }
        public decimal HeightSm { get; set; }
        public short WidthStitches { get; set; }
        public short HeightStitches { get; set; }
        public int? AuthorId { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }

        public PatternAuthor Author { get; set; }
        public FabricItem FabricItem { get; set; }
        public KitManufacturer Manufacturer { get; set; }
        public ThreadManufacturer ThreadManufacturer { get; set; }
    }
}
