using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainSI.Models
{
    public class BatchTempRangeViewModel
    {
        [Key]
        public string RangeId { get; set; }

        public string BatchId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Min Temp")]
        public decimal MinTemp { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Max Temp")]
        public decimal MaxTemp { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Max Tick Counts")]
        public decimal ExpireTickCount { get; set; }
    }
}