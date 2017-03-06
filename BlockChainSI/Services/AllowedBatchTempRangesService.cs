using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChainSI.Models;
using BlockChainHot.Repository;
using AutoMapper;

namespace BlockChainSI.Services
{
    public class AllowedBatchTempRangesService : BaseService, IAllowedBatchTempRangesService
    {
        private BlockChainHotDBContext dbContext;
        public AllowedBatchTempRangesService()
        {
            dbContext = new BlockChainHotDBContext();
        }
        public StabilityRangeListViewModel GetBatchAllowedTempDetails(string batchCode)
        {
            var allowedTempRangeListModel = new StabilityRangeListViewModel();
            var allowedTempRangesList = dbContext.StabilityRanges.Where(x => x.BatchCode == batchCode).OrderBy(x => x.RangeId).ToList();
            var allowedTemperatureRanges = Mapper.Map<IList<StabilityRange>, List<StabilityRangeViewModel>>(allowedTempRangesList);
            if (allowedTemperatureRanges == null || allowedTemperatureRanges.Count() == 0)
            {
                var allowedTemperatureSet1 = new StabilityRangeViewModel()
                {
                    BatchCode = batchCode,
                    Id = 1,
                    RangeId = 1,
                };
                allowedTemperatureRanges.Add(allowedTemperatureSet1);
            }
            allowedTempRangeListModel.AllowedTemperatureRanges = allowedTemperatureRanges;
            allowedTempRangeListModel.BatchId = batchCode;

            return allowedTempRangeListModel;
        }

        public bool UpdateAllowedBatchTemp(StabilityRangeListViewModel allowedTempRanges)
        {
            var existingTempRanges = dbContext.StabilityRanges.Where(x => x.BatchCode == allowedTempRanges.BatchId).ToList();
            dbContext.StabilityRanges.RemoveRange(existingTempRanges);
            if (allowedTempRanges.AllowedTemperatureRanges != null && allowedTempRanges.AllowedTemperatureRanges.Count > 0)
            {
                var allowedTempRangesToUpdate = Mapper.Map<IList<StabilityRangeViewModel>, 
                                                    List<StabilityRange>>(allowedTempRanges.AllowedTemperatureRanges);
                dbContext.StabilityRanges.AddRange(allowedTempRangesToUpdate);
            }
            return dbContext.SaveChanges() > 0;
        }
    }
}
