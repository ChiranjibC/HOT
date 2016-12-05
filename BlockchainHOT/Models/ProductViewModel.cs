﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlockchainHOT.Models
{
    public class ProductViewModel
    {
        [Key]
        public Guid ProductId { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "Product Description")]
        public string ProductDesc { get; set; }
        
    }
}