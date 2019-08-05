using System;

namespace Web.Utils
{
    public class SizeCalculator
    {
        public static string SizeInSm(decimal w, decimal h, string name)
        {
            decimal stitchCount;
            switch (name)
            {
                case "Aida 14":
                case "Monika 28 ct.":
                case "Eva 28 ct.":
                    {
                        stitchCount = 14;
                        break;
                    }
                case "Aida 16":
                case "Belfast":
                case "Murano":
                    {
                        stitchCount = 16;
                        break;
                    }
                case "Linda":
                    {
                        stitchCount = 13.5m;
                        break;
                    }
                case "Aida 18":
                case "Edinburgh":
                    {
                        stitchCount = 18;
                        break;
                    }
                default:
                {
                    throw new Exception();
                }
            }

            var wSm = w / stitchCount * 2.54m;
            var hSm = h / stitchCount * 2.54m;
            return $"{wSm:F1} x {hSm:F1} sm";
        }
    }
}
