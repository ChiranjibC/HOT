using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Models
{
    public class TempRangeViewModel
    {

        [Key]
        public Guid TempRangeId { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Temperature Range Code")]
        public string TempRangeCode { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Temperature Range")]
        public string TempRange { get; set; }

    }
}
