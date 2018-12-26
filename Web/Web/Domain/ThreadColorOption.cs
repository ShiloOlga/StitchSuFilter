namespace Web.Domain
{
    public partial class ThreadColorOption
    {
        public int PatternId { get; set; }
        public int ThreadColorId { get; set; }
        public decimal? RequiredLength { get; set; }

        public Pattern Pattern { get; set; }
        public ThreadColor ThreadColor { get; set; }
    }
}
