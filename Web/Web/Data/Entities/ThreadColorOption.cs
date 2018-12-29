namespace Web.Data.Entities
{
    public partial class ThreadColorOption
    {
        public int PatternId { get; set; }
        public int ThreadColorId { get; set; }
        public decimal? RequiredLength { get; set; }

        public virtual Pattern Pattern { get; set; }
        public virtual ThreadColor ThreadColor { get; set; }
    }
}
