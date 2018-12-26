namespace Web.Domain
{
    public partial class FabricOption
    {
        public int PatternId { get; set; }
        public int FabricItemId { get; set; }

        public FabricItem FabricItem { get; set; }
        public Pattern Pattern { get; set; }
    }
}
