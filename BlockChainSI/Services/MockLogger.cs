using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockChainSI.Models;

namespace BlockChainSI.Mock
{
    public class MockLogger : MockData, ILogger
    {
        private static IList<BatchViewModel> _batchList = MockBatch.batchList;
        private static IList<TempLoggerViewModel> _deviceList = MockTempLogger.deviceList;
        private static IList<TempRangeViewModel> _tempRangeList = MockTempRange.tempRangeList;
        public IEnumerable<LoggerViewModel> GetLogList(int pageSize, int pageNo)
        {
            return GetLogList(pageSize);
        }

        public LoggerViewModel UpdateLogItem(LoggerViewModel logItem)
        {
            logItem.LoggerId = Guid.NewGuid();
            return logItem;
        }

        public bool UpdateLogList(IList<LoggerViewModel> logList)
        {
            return true;
        }
        public LoggerViewModel GetDetails(Guid id)
        {
            return GetLoggerItem(id);
        }
        private IEnumerable<LoggerViewModel> GetLogList(int count)
        {
            var batchLists = new List<LoggerViewModel>();
            for (int i = 0; i < count; i++)
            {
                batchLists.Add(GetLoggerItem(Guid.NewGuid()));
            }
            return batchLists;
        }

        private LoggerViewModel GetLoggerItem(Guid guid)
        {
            var firstTempRange = GetRandInt();
            var secondTempRange = GetRandInt(firstTempRange, firstTempRange * 10);
            var batch = new LoggerViewModel()
            {
                TempLogger = _deviceList[GetRandInt(0, _deviceList.Count - 1)],
                Batch = _batchList[GetRandInt(0, _batchList.Count - 1)],
                TempRange = _tempRangeList[GetRandInt(0, _tempRangeList.Count - 1)],
                LoggerId = guid,
                DurationInMins = GetRandInt(),
                Stage = "WareHouse",
                RecordDateTime = DateTime.Now.AddDays(-1 * GetRandInt()),
            };
            return batch;
        }
    }
}
