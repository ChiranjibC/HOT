using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlockchainHOT.Models
{
    public class DeviceViewModel
    {
        [Key]
        public string DeviceId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Device #")]
        public string DeviceNo { get; set; }

        [Required]
        [DataType("BlockchainHOT.Models.DeviceFamilyViewModel")]
        [Display(Name = "Device Family #")]
        public DeviceFamilyViewModel DeviceFamily { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Device Short Name")]
        public string DeviceName { get; set; }

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