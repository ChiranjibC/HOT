using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlockChainSI.Models
{
    public class MapperViewModel
    {
        [Key]
        public Guid MapId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Batch #")]
        public string BatchNumber { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Device #")]
        public string DeviceNo { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        
    }
}