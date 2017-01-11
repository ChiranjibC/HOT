using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        [Display(Name = "Batch #")]
        public BatchViewModel Batch { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Record Date")]
        public DateTime RecordDateTime { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Temp Logger #")]
        public TempLoggerViewModel TempLogger { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Stage")]
        public string Stage { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Temperature Range")]
        public TempRangeViewModel TempRange { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Duration (Min)")]
        public int DurationInMins { get; set; }


        public SelectList BatchList { get; set; }
        public SelectList DeviceList { get; set; }
        public SelectList TempRangeList { get; set; }
    }
}
