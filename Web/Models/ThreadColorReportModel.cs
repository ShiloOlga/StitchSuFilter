using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data.Entities;

namespace Web.Models
{
    public class ThreadColorReportModel
    {
        public ThreadColor Color { get; set; }
        public decimal TotalLength { get; set; }
    }
}
