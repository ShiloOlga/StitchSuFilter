using Newtonsoft.Json;
using Web.Data;

namespace Web.Models.V2
{
    public class PatternModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Item { get; set; }
        public Size Size { get; set; }
        [JsonProperty("Fabric")]
        public FabricItemModel[] Fabrics { get; set; }
        public string ImageUrl { get; set; }
        public int ColorsCount { get; set; }
        public string Link { get; set; }
    }
}
