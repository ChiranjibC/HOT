using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainSI.Models
{
    public class TemparatureTelemetryViewModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Batch")]
        public string BatchCode { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Batch")]
        public string BatchDescription { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Temperature")]
        public int Temperature { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Log Time")]
        public DateTime LogTime { get; set; }

        public SelectList BatchList { get; set; }

    }
}