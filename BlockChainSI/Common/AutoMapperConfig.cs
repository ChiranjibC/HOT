using AutoMapper;
using BlockChainHot.Repository;
using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Common
{
    public static class AutoMapperConfig
    {
        public static void Configuration()
        {
            Mapper.Initialize(cnf =>
            {
                cnf.CreateMap<StabilityRange, StabilityRangeViewModel>();
                cnf.CreateMap<StabilityRangeViewModel, StabilityRange>();
                cnf.CreateMap<Batch, BatchViewModel>()
                            .ForMember(dest => dest.IsExpired, opt => opt.MapFrom(x => x.ExpiryStatus ? "Yes" : "No"))
                            .ForMember(dest => dest.TempLogger, opt => opt.Ignore())
                            .ForMember(dest => dest.CurrentOwner, opt => opt.Ignore())
                            .ForMember(dest => dest.Producer, opt => opt.Ignore())
                            .ForMember(dest => dest.OwnershipDetails, opt => opt.Ignore())
                            .ForMember(dest => dest.OwnerList, opt => opt.Ignore());
                cnf.CreateMap<BatchViewModel, Batch>();
                cnf.CreateMap<BatchOwnershipHistory, BatchOwnershipHistoryViewModel>()
                            .ForMember(dest => dest.Batch, opt => opt.Ignore())
                            .ForMember(dest => dest.BatchList, opt => opt.Ignore())
                            .ForMember(dest => dest.OwnerList, opt => opt.Ignore())
                            .ForMember(dest => dest.Owner, opt => opt.Ignore());
                cnf.CreateMap<BatchOwnershipHistoryViewModel, BatchOwnershipHistory>();
                cnf.CreateMap<OwnerManager, OwnerViewModel>();
                cnf.CreateMap<OwnerViewModel, OwnerManager>();
                cnf.CreateMap<TemparatureTelemetry, TemparatureTelemetryViewModel>();
                cnf.CreateMap<TemparatureTelemetryViewModel, TemparatureTelemetry>();

            });
        }
    }
}
