using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlockchainHOT.Models
{
    public class StabilityChartViewModel
    {
        [Key]
        public Guid StabilityId { get; set; }

        public Guid BatchId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "From Temperature")]
        public string FromTemp { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "To Temperature")]
        public string ToTemp { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Allowed Time in Minutes")]
        public double AllowedTimeInMinutes { get; set; }
    }
}