using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainSI.Models
{
    public class MapperViewModel
    {
        [Key]
        public Guid MapId { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Batch #")]
        //public string BatchNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Batch #")]
        public BatchViewModel Batch { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "Device #")]
        //public string DeviceNo { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Device #")]
        public TempLoggerViewModel TempLogger { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }


        [DataType(DataType.Text)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        public SelectList BatchList { get; set; }
        public SelectList DeviceList { get; set; }

    }
}