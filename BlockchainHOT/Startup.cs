using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlockchainHOT.Startup))]
namespace BlockchainHOT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
