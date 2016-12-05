using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlockchainHOT.Models
{
    public class BatchViewModel
    {
        [Key]
        public Guid BatchId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Batch #")]
        public string BatchNumber { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Batch Description")]
        public string BatchDesc { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Product")]
        public string Product { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Quantity")]
        public string Quantity { get; set; }

        //public IEnumerable<StabilityChartViewModel> StabilityDetails { get; set; }

        public static explicit operator BlockChainSI.Models.BatchViewModel(BatchViewModel batchView)
        {
            return new BlockChainSI.Models.BatchViewModel();
        }

        public static explicit operator BatchViewModel(BlockChainSI.Models.BatchViewModel batchView)
        {
            return new BatchViewModel();
        }
    }
}