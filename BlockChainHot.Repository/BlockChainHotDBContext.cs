using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainHot.Repository
{
    public class BlockChainHotDBContext : DbContext
    {
        public BlockChainHotDBContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<StabilityRange> StabilityRanges { get; set; }
        public DbSet<Batch> Batch { get; set; }
        public DbSet<BatchOwnershipHistory> BatchOwnershipHistory { get; set; }
        public DbSet<TemparatureTelemetry> TemparatureTelemetry { get; set; }
        public DbSet<OwnerManager> OwnerManager { get; set; }
    }
}
