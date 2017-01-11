using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlockChainHot.Repository
{
    [Table("OwnerManager")]
    public class OwnerManager
    {
        public long Id { get; set; }
        public string OwnerCode { get; set; }
        public string OwnerDesc { get; set; } 
        public bool IsTempLogger { get; set; }
    }
}