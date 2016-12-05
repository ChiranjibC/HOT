using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Contracts
{
    public interface IStability
    {
        IEnumerable<StabilityChartViewModel> GetProductStabilityDetails(Guid productId);
        StabilityChartViewModel UpdateProductStability(StabilityChartViewModel stability);
        bool DeleteStabilityItem(Guid stabilityId);
    }
}
