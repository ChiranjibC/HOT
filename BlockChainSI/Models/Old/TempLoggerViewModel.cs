using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlockChainSI.Models
{
    public class TempLoggerViewModel
    {
        [Key]
        public Guid TempLoggerId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Temp Logger #")]
        public string TempLoggerNo { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Temp Logger Name")]
        public string TempLoggerShortName { get; set; }

        [Required]
        [DataType(DataType.Duration)]
        [Display(Name = "Log Interval")]
        public double LogInterval { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        
    }
}