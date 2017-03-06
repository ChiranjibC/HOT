using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainSI.Models
{
    public class BatchViewModel
    {
        public BatchViewModel()
        {
            OwnerList = new SelectList("--Select--");
            OwnershipDetails = new List<BatchOwnershipHistoryViewModel>();
        }

        [Key]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Batch #")]
        public string BatchCode { get; set; }

        [DataType(DataType.Text)]
        [StringLength(16)] //as this is sent to BlockChain Bytes32
        [Display(Name = "Batch")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Link Temperature Logger")]
        public string TempLoggerCode { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Temp Logger")]
        public OwnerViewModel TempLogger { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Current Owner")]
        public string CurrentOwnerCode { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Tracked By")]
        public OwnerViewModel CurrentOwner { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Linked By")]
        public string ProducerCode { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Linked By")]
        public OwnerViewModel Producer { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Is Expired")]
        public string IsExpired { get; set; }
        
        public SelectList OwnerList { get; set; }
        public SelectList TempLoggerList { get; set; }
        public List<BatchOwnershipHistoryViewModel> OwnershipDetails { get; set; }

    }
}