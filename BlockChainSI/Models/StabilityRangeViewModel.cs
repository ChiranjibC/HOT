using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainSI.Models
{
    public class StabilityRangeViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Batch")]
        public string BatchCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Range Id")]
        public int RangeId { get; set; }

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
        [Display(Name = "Max Tick Counts (~15 Mins)")]
        public decimal ExpireTickCount { get; set; }

        public SelectList Batches { get; set; }

    }
}