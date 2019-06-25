using System.Collections.Generic;
using Web.Data;

namespace Web.Models
{
    public class PatternModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public AuthorModel Author { get; set; }
        public string Item { get; set; }
        public Size Size { get; set; }
        public string ImageUrl { get; set; }
        public int ColorsCount { get; set; }
        public string Link { get; set; }
        public Dictionary<PaletteModel, IEnumerable<ColorModel>> ColorsMap { get; set; }
        public ICollection<FabricModel> FabricItems { get; set; }
    }
}
