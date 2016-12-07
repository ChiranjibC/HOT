using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainSI.Models
{
    public class StabilityChartViewModel
    {
        [Key]
        public Guid StabilityId { get; set; }

        public Guid ProductId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "From Temperature")]
        public decimal FromTemp { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "To Temperature")]
        public decimal ToTemp { get; set; }        

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Temperature Range")]
        public TempRangeViewModel TempRange { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Allowed Time in Minutes")]
        public double AllowedTimeInMinutes { get; set; }

        public SelectList TempRanges { get; set; }
    }
}