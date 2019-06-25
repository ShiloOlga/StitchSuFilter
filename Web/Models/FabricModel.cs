namespace Web.Models
{
    public class FabricModel
    {
        public string Name { get; set; }
        public sbyte Count { get; set; }
        public string Sku { get; set; }

        public override string ToString()
        {
            return $"{Name} {Count} count, {Sku}";
        }
    }
}
