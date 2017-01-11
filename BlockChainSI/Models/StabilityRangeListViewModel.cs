using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainSI.Models
{
    public class StabilityRangeListViewModel
    {
        public string BatchId { get; set; }
        public List<StabilityRangeViewModel> AllowedTemperatureRanges { get; set; }
    }
}