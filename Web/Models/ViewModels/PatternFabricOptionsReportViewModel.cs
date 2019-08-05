using System.Collections.Generic;
using System.Linq;
using Web.Models.V2;
using Web.Utils;

namespace Web.Models.ViewModels
{
    public class PatternFabricOptionsReportViewModel
    {
        private PatternFabricOptionsReportViewModel() { }
        private static readonly FabricItemModelEqualityComparer _comparer = new FabricItemModelEqualityComparer();

        public List<FabricPatterns> Lines { get; set; }

        public static PatternFabricOptionsReportViewModel Build(IEnumerable<V2.PatternModel> sourceItems)
        {
            var model = new PatternFabricOptionsReportViewModel { Lines = new List<FabricPatterns>() };
            var items = sourceItems
                .Where(x => x.FabricOptions!= null && x.FabricOptions.Any())
                .ToArray();
            var options = items
                .SelectMany(x => x.FabricOptions)
                .Distinct(_comparer)
                .OrderBy(x => x.Name)
                .ToArray();
            foreach (var option in options)
            {
                var patterns = items.Where(x => x.FabricOptions.Contains(option, _comparer));
                model.Lines.Add(new FabricPatterns
                {
                    Name = $"{option.Name}, {option.Color} ({option.ColorName})",
                    Patterns = patterns.Select(x => $"{x.Title}, {SizeCalculator.SizeInSm(x.Size.Width, x.Size.Height, option.Name)}")
                });
            }

            return model;
        }
    }

    public class FabricPatterns
    {
        public string Name { get; set; }
        public IEnumerable<string> Patterns { get; set; }
    }
}
