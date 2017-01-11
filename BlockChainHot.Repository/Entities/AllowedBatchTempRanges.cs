namespace BlockChainHot.Repository
{
    public class AllowedBatchTempRanges
    {
        public long Id { get; set; }
        public string BatchId { get; set; }
        public int RangeId { get; set; }
        public int MinTemp { get; set; }
        public int MaxTemp { get; set; }
        public int ExpireTickCount { get; set; }        
    }
}