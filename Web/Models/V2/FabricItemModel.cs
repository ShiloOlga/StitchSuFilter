namespace Web.Models.V2
{
    public class FabricItemModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string ColorName { get; set; }
        public FabricModel Fabric { get; set; }
    }
}
