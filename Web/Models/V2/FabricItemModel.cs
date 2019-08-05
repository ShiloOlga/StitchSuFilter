using System.Collections.Generic;

namespace Web.Models.V2
{
    public class FabricItemModel
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string ColorName { get; set; }
        public FabricModel Fabric { get; set; }
    }

    internal class FabricItemModelEqualityComparer : IEqualityComparer<FabricItemModel>
    {
        public bool Equals(FabricItemModel x, FabricItemModel y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }
            return x.Name == y.Name && x.Color == y.Color && x.ColorName == y.ColorName;
        }

        public int GetHashCode(FabricItemModel o)
        {
            if (o == null)
            {
                return 0;
            }
            return o.Name.GetHashCode() ^ o.Color.GetHashCode() ^ o.ColorName.GetHashCode();
        }
    }
}