using System.Collections.Generic;

namespace Web.Data.Entities
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

        public virtual PatternAuthor Author { get; set; }
        public virtual ICollection<FabricOption> FabricOptions { get; set; }
        public virtual ICollection<ThreadColorOption> ThreadColorOptions { get; set; }
    }
}
