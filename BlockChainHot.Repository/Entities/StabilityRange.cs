using System.ComponentModel.DataAnnotations.Schema;

namespace BlockChainHot.Repository
{
    [Table("StabilityRange")]
    public class StabilityRange
    {
        public long Id { get; set; }
        public string BatchCode { get; set; }
        public int RangeId { get; set; }
        public int MinTemp { get; set; }
        public int MaxTemp { get; set; }
        public int ExpireTickCount { get; set; }        
    }
}