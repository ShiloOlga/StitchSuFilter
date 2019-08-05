using System.Collections.Generic;

namespace Web.Data.Repositories
{
    public interface ICrossStitchPatternsRepository
    {
        ICollection<Models.V2.PatternModel> Patterns { get; }
    }
}
