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

        public virtual PatternAuthor Author { get; set; }
        public virtual FabricItem FabricItem { get; set; }
        public virtual KitManufacturer Manufacturer { get; set; }
        public virtual ThreadManufacturer ThreadManufacturer { get; set; }
    }
}
