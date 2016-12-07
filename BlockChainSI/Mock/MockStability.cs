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

        public IList<StabilityChartViewModel> GetProductStabilityDetails(Guid productId)
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
        
        private IList<StabilityChartViewModel> GetStabilityDetails(Guid productId)
        {
            var tempRanges = MockTempRange.tempRangeList;
            var stabilityDetails = new List<StabilityChartViewModel>();
            stabilityDetails.Add(new StabilityChartViewModel()
            {
                AllowedTimeInMinutes = GetRandInt(10),                
                StabilityId = Guid.NewGuid(),
                ProductId = productId,
                TempRange = tempRanges[0]
            });

            stabilityDetails.Add(new StabilityChartViewModel()
            {
                AllowedTimeInMinutes = GetRandInt(15),
                TempRange = tempRanges[1],
                StabilityId = Guid.NewGuid(),
                ProductId = productId,
            });

            stabilityDetails.Add(new StabilityChartViewModel()
            {
                AllowedTimeInMinutes = 0,
                TempRange = tempRanges[2],
                StabilityId = Guid.NewGuid(),
                ProductId = productId,
            });

            //stabilityDetails.Add(new StabilityChartViewModel()
            //{
            //    AllowedTimeInMinutes = GetRandInt(35),
            //    TempRange = tempRanges[3],
            //    StabilityId = Guid.NewGuid(),
            //    ProductId = productId,
            //});

            return stabilityDetails;
        }
    }
}
