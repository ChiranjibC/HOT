using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using BlockChainSI.Contracts;
using BlockChainSI.Mock;

namespace BlockchainHOT
{
    public static class UnityConfig
    {
        public static InjectionMember[] Batch { get; private set; }

        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            container.RegisterType<IProduct, MockProduct>();
            container.RegisterType<IStability, MockStability>();
            container.RegisterType<ITempLogger, MockTempLogger>();
            container.RegisterType<IBatch, MockBatchDB>(); //using the database mock
            container.RegisterType<IMapper, MockMapper>();
            container.RegisterType<ILogger, MockLogger>();
            container.RegisterType<ITempRange, MockTempRange>();
            container.RegisterType<IAllowedBatchTempRanges, MockAllowedTempRanges>();
            container.RegisterType<IOwnership, MockOwnership>();
            container.RegisterType<ITempTelemetry, MockTempTelemetry>();
        }
    }
}