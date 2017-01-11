using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlockChainHot.Repository
{
    [Table("TemparatureTelemetry")]
    public class TemparatureTelemetry
    {
        public long Id { get; set; }
        public string BatchCode { get; set; }
        public int Temperature { get; set; }
        public DateTime LogTime { get; set; }
    }
}