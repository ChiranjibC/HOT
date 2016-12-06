using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChainSI.Models;

namespace BlockChainSI.Mock
{
    public class MockStability : MockData, IStability
    {
        public bool DeleteStabilityItem(Guid stabilityId)
        {
            return true;
        }

        public IEnumerable<StabilityChartViewModel> GetProductStabilityDetails(Guid productId)
        {
            return GetStabilityDetails(productId);
        }

        public StabilityChartViewModel UpdateProductStability(StabilityChartViewModel stability)
        {
            stability.StabilityId = Guid.NewGuid();
            return stability;
        }

        public bool UpdateAllProductStability(IList<StabilityChartViewModel> productStability)
        {
            return true;
        }

        private IEnumerable<StabilityChartViewModel> GetStabilityDetails(Guid productId)
        {
            var stabilityDetails = new List<StabilityChartViewModel>();
            stabilityDetails.Add(new StabilityChartViewModel()
            {
                AllowedTimeInMinutes = GetRandInt(10),
                FromTemp = 25,
                ToTemp = 999,
                StabilityId = Guid.NewGuid(),
                ProductId = productId,
            });

            stabilityDetails.Add(new StabilityChartViewModel()
            {
                AllowedTimeInMinutes = GetRandInt(15),
                FromTemp = 15,
                ToTemp = 25,
                StabilityId = Guid.NewGuid(),
                ProductId = productId,
            });

            stabilityDetails.Add(new StabilityChartViewModel()
            {
                AllowedTimeInMinutes = 0,
                FromTemp = 9,
                ToTemp = 15,
                StabilityId = Guid.NewGuid(),
                ProductId = productId,
            });

            stabilityDetails.Add(new StabilityChartViewModel()
            {
                AllowedTimeInMinutes = GetRandInt(35),
                FromTemp = 1,
                ToTemp = 9,
                StabilityId = Guid.NewGuid(),
                ProductId = productId,
            });

            stabilityDetails.Add(new StabilityChartViewModel()
            {
                AllowedTimeInMinutes = GetRandInt(45),
                FromTemp = 0,
                ToTemp = 1,
                StabilityId = Guid.NewGuid(),
                ProductId = productId,
            });

            stabilityDetails.Add(new StabilityChartViewModel()
            {
                AllowedTimeInMinutes = GetRandInt(15),
                FromTemp = -999,
                ToTemp = 0,
                StabilityId = Guid.NewGuid(),
                ProductId = productId,
            });
            return stabilityDetails;
        }
    }
}
