using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainSI.Models
{
    public class BatchOwnershipHistoryViewModel
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
        public BatchViewModel Batch { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Tracked By")]
        public string OwnerCode{ get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Tracker")]
        public OwnerViewModel Owner { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Assigned From")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "End Time")]
        public DateTime? EndTime { get; set; }

        public SelectList BatchList { get; set; }
        public SelectList OwnerList { get; set; }
    }
}