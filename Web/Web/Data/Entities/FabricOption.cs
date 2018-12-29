namespace Web.Data.Entities
{
    public partial class FabricOption
    {
        public int PatternId { get; set; }
        public int FabricItemId { get; set; }

        public virtual FabricItem FabricItem { get; set; }
        public virtual Pattern Pattern { get; set; }
    }
}
