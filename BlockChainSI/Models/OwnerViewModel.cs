using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockChainSI.Models
{
    public class OwnerViewModel
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Owner")]
        public string OwnerCode { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Owner Description")]
        public string OwnerDesc { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Is Temp Logger")]
        public bool IsTempLogger { get; set; }
    }
}