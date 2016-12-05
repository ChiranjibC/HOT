using Microsoft.AspNet.Identity.EntityFramework;

namespace BlockchainHOT.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public System.Data.Entity.DbSet<BlockChainSI.Models.DeviceViewModel> DeviceViewModels { get; set; }

        public System.Data.Entity.DbSet<BlockChainSI.Models.BatchViewModel> BatchViewModels { get; set; }

        public System.Data.Entity.DbSet<BlockChainSI.Models.ProductViewModel> ProductViewModels { get; set; }

        public System.Data.Entity.DbSet<BlockChainSI.Models.StabilityChartViewModel> StabilityChartViewModels { get; set; }
    }
}