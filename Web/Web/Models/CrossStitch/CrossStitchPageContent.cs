﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models.CrossStitch
{
    public class CrossStitchPageContent
    {
        public IEnumerable<StitchSuPatternModel> Patterns { get; set; }
        public int PageCount { get; set; }
    }
}