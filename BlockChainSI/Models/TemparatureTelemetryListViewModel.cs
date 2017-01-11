using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainSI.Models
{
    public class TemparatureTelemetryListViewModel
    {
        public IList<TemparatureTelemetryViewModel> TempTelemetryList { get; set; }

        public SelectList BatchList { get; set; }

    }
}