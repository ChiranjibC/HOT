using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlockChainSI.Models
{
    public class LoggerViewModel
    {
        [Key]
        public Guid LoggerId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Batch #")]
        public string BatchNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Record Date")]
        public DateTime RecordDateTime { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Device #")]
        public string DeviceNo { get; set; }

        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Stage")]
        public string Stage { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Temperature Range")]
        public string TemperatureRange { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Duration (Min)")]
        public int DurationInMins { get; set; }
    }
}
