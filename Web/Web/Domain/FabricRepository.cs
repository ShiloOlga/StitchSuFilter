using System.Collections.Generic;

namespace Web.Domain
{
    public class FabricRepository
    {
        public IEnumerable<Fabric> Fabrics { get; }

        public FabricRepository()
        {
            Fabrics = new List<Fabric>
            {
                new Fabric("Aida 16", 16, FabricType.Blockweare, 1, "100% cotton"),
                new Fabric("Aida 14", 14, FabricType.Blockweare, 1, "100% cotton"),
                new Fabric("Aida 18", 18, FabricType.Blockweare, 2, "100% cotton"),
                new Fabric("Aida 11", 11, FabricType.Blockweare, 3, "100% cotton"),
                new Fabric("Linda", 27, FabricType.Evenweare, 1, "100% cotton"),
                new Fabric("Jubilee", 28, FabricType.Evenweare, 3, "100% cotton"),
                new Fabric("Annabelle", 28, FabricType.Evenweare, 3, "100% cotton"),
                new Fabric("Dublin", 20, FabricType.Evenweare, 3, "100% linen"),
                new Fabric("Cashel", 28, FabricType.Evenweare, 2, "100% linen"),
                new Fabric("Belfast", 32, FabricType.Evenweare, 2, "100% linen"),
                new Fabric("Permin", 32, FabricType.Evenweare, 2, "100% linen"),
                new Fabric("Edinburgh", 36, FabricType.Evenweare, 3, "100% linen"),
                new Fabric("Newcastle", 40, FabricType.Evenweare, 3, "100% linen"),
                new Fabric("Lugana", 25, FabricType.Evenweare, 1, "52% cotton & 48% rayon"),
                new Fabric("Murano", 32, FabricType.Evenweare, 1, "52% cotton & 48% rayon"),
                new Fabric("Bellana", 20, FabricType.Evenweare, 3, "52% cotton & 48% rayon"),
                new Fabric("Perlleinen80", 20, FabricType.Evenweare, 3, "60% polyester & 40% linen"),
                new Fabric("Perlleinen100", 25, FabricType.Evenweare, 3, "60% polyester & 40% linen"),
                new Fabric("Perlleinen", 32, FabricType.Evenweare, 3, "52% cotton & 48% rayon"),
                new Fabric("Brittney", 28, FabricType.Evenweare, 2, "52% cotton & 48% rayon"),
                new Fabric("Lucan", 32, FabricType.Evenweare, 2, "48% cotton & 52% linen")
            };
        }
    }
}
