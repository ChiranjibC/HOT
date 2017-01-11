using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlockChainSI.Models
{
    public class DeviceFamilyViewModel
    {
        [Key]
        public Guid DeviceFamilyId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Device Family #")]
        public string DeviceFamilyNo { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Device Family")]
        public string DeviceFamilyName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Device Description")]
        public string DeviceFamilyDesc { get; set; }
    }
}