using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChainSI.Models;

namespace BlockChainSI.Mock
{
    public class MockTempRange : MockData, ITempRange
    {

        private static List<TempRangeViewModel> _tempRangeList = null;
        public static List<TempRangeViewModel> tempRangeList
        {
            get
            {
                if (_tempRangeList == null)
                {
                    _tempRangeList = GetTempRangeList(100);
                }
                return _tempRangeList;
            }
        }

        public IEnumerable<TempRangeViewModel> GetTempRanges()
        {
            return tempRangeList;
        }
        public TempRangeViewModel GetTempRange(Guid guid)
        {
            return tempRangeList.Where(x => x.TempRangeId == guid).FirstOrDefault();
        }
                        
        private static List<TempRangeViewModel> GetTempRangeList(int pageSize)
        {
            List<TempRangeViewModel> tempRanges = new List<TempRangeViewModel>();
            
            tempRanges.Add(new TempRangeViewModel()
            {
                TempRangeId = Guid.NewGuid(),
                TempRangeCode = "TR1",
                TempRange = "25 - 999"
            });

            tempRanges.Add(new TempRangeViewModel()
            {
                TempRangeId = Guid.NewGuid(),
                TempRangeCode = "TR1",
                TempRange = "10 - 25"
            });

            tempRanges.Add(new TempRangeViewModel()
            {
                TempRangeId = Guid.NewGuid(),
                TempRangeCode = "TR1",
                TempRange = "1 - 10"
            });

            tempRanges.Add(new TempRangeViewModel()
            {
                TempRangeId = Guid.NewGuid(),
                TempRangeCode = "TR1",
                TempRange = "-9999 - 0"
            });

            tempRanges.Add(new TempRangeViewModel()
            {
                TempRangeId = Guid.NewGuid(),
                TempRangeCode = "TR2",
                TempRange = "21 - 999"
            });

            tempRanges.Add(new TempRangeViewModel()
            {
                TempRangeId = Guid.NewGuid(),
                TempRangeCode = "TR2",
                TempRange = "12 - 21"
            });

            tempRanges.Add(new TempRangeViewModel()
            {
                TempRangeId = Guid.NewGuid(),
                TempRangeCode = "TR2",
                TempRange = "3 - 12"
            });

            tempRanges.Add(new TempRangeViewModel()
            {
                TempRangeId = Guid.NewGuid(),
                TempRangeCode = "TR2",
                TempRange = "-10 - 3"
            });

            tempRanges.Add(new TempRangeViewModel()
            {
                TempRangeId = Guid.NewGuid(),
                TempRangeCode = "TR2",
                TempRange = "-9999 - -10"
            });

            return tempRanges;
        }
    }
}
