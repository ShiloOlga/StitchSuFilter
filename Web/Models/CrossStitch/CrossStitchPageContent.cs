using System.Collections.Generic;

namespace Web.Models.CrossStitch
{
    public class CrossStitchPageContent
    {
        public IEnumerable<CrossStitchPatternModel> Patterns { get; set; }
        public IEnumerable<WishlistKitModel> Kits { get; set; }
    }
}
