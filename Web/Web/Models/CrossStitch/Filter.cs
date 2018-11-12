using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.CrossStitch
{
    public class Filter
    {
        public string Author { get; set; }
        public PatternDistributionStatus Status { get; set; }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(Author);
            }
        }
    }
}
