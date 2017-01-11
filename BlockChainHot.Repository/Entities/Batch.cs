using System.ComponentModel.DataAnnotations.Schema;

namespace BlockChainHot.Repository
{
    [Table("Batch")]
    public class Batch
    {
        public long Id { get; set; }
        public string BatchCode { get; set; }
        public string TempLoggerCode { get; set; }
        public string Description { get; set; }
        public string ProducerCode { get; set; }
        public bool ExpiryStatus { get; set; }
    }
}