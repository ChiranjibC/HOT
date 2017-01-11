using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainSI.Models
{
    public class _BatchViewModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Batch #")]
        public string BatchId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Device Id")]
        public string DeviceId { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Device")]
        public OwnerViewModel Device { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Current Owner Id")]
        public string CurrentOwnerId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Current Owner")]
        public OwnerViewModel CurrentOwner { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "ProducerId")]
        public string ProducerId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Producer")]
        public OwnerViewModel Producer { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Is Expired")]
        public string IsExpired { get; set; }
        
        public SelectList OwnerList { get; set; }


        //*************************************************not in use, left for backwards compatibility
        [DataType(DataType.Text)]
        [Display(Name = "Product")]
        public ProductViewModel Product { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Quantity")]
        public string Quantity { get; set; }
        
        public SelectList Products { get; set; }
    }
}