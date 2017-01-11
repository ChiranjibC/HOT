using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlockChainHot.Repository
{
    [Table("BatchOwnershipHistory")]
    public class BatchOwnershipHistory
    {
        public long Id { get; set; }
        public string BatchCode { get; set; }
        public string OwnerCode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}