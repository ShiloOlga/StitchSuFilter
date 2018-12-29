using System.Collections.Generic;

namespace Web.Models
{
    public class PatternColorModel
    {
        public PaletteModel Palette { get; set; }
        public ICollection<ColorModel> Colors { get; set; }
    }

    public class PaletteModel
    {
        public string Name { get; set; }
    }

    public class ColorModel
    {
        public string Color { get; set; }
        public decimal Length { get; set; }

        public override string ToString()
        {
            return $"{Color}, {Length:F2}m";
        }
    }
}
