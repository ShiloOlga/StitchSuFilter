namespace Web.Domain
{
    public class Size
    {
        public decimal Width { get; set; }
        public decimal Height { get; set; }

        public override string ToString()
        {
            return $"{Width}x{Height}";
        }
    }
}
