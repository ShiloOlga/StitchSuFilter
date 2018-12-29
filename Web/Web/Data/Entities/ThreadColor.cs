using System.Collections.Generic;

namespace Web.Data.Entities
{
    public partial class ThreadColor
    {
        public ThreadColor()
        {
            ThreadColorOptions = new HashSet<ThreadColorOption>();
        }

        public int Id { get; set; }
        public int? ManufacturerId { get; set; }
        public string Sku { get; set; }
        public string ColorId { get; set; }
        public string ColorName { get; set; }
        public string RgbColor { get; set; }
        public sbyte Length { get; set; }

        public virtual ThreadManufacturer Manufacturer { get; set; }
        public virtual ICollection<ThreadColorOption> ThreadColorOptions { get; set; }
    }
}
