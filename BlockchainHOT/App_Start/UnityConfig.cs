using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using BlockChainSI.Contracts;
using BlockChainSI.Services;

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
            container.RegisterType<IBatchService, BatchService>();            
            container.RegisterType<IAllowedBatchTempRangesService, AllowedBatchTempRangesService>();
            container.RegisterType<IOwnershipService, OwnershipService>();
            container.RegisterType<ITempTelemetryService, TempTelemetryService>();
        }
    }
}