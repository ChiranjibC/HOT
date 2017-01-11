using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainSI.Models
{
    public class _OwnerViewModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Owner Id")]
        public string OwnerId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Owner Description")]
        public string OwnerDesc { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        
        public SelectList Products { get; set; }
    }
}