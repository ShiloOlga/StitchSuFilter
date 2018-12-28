using System.Collections.Generic;

namespace Web.Domain
{
    public partial class Pattern
    {
        public Pattern()
        {
            FabricOptions = new HashSet<FabricOption>();
            ThreadColorOptions = new HashSet<ThreadColorOption>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Item { get; set; }
        public int ColorsCount { get; set; }
        public short Width { get; set; }
        public short Height { get; set; }
        public int? AuthorId { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }

        public PatternAuthor Author { get; set; }
        public ICollection<FabricOption> FabricOptions { get; set; }
        public ICollection<ThreadColorOption> ThreadColorOptions { get; set; }
    }
}
