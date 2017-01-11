using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainSI.Models
{
    public class StabilityListViewModel
    {
        public IList<StabilityChartViewModel> StabilityDetails { get; set; }

        public SelectList TempRangeList { get; set; }
    }
}