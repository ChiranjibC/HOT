using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Contracts
{
    public interface IAllowedBatchTempRangesService
    {
        StabilityRangeListViewModel GetBatchAllowedTempDetails(string batchId);
        bool UpdateAllowedBatchTemp(StabilityRangeListViewModel allowedTempList);
    }
}
