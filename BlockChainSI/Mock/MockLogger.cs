﻿using BlockChainSI.Contracts;
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

        private IEnumerable<LoggerViewModel> GetLogList(int count)
        {
            var batchLists = new List<LoggerViewModel>();
            for (int i = 0; i < count; i++)
            {
                batchLists.Add(GetLoggerItem());
            }
            return batchLists;
        }

        private LoggerViewModel GetLoggerItem()
        {
            var firstTempRange = GetRandInt();
            var secondTempRange = GetRandInt(firstTempRange, firstTempRange * 10);
            var batch = new LoggerViewModel()
            {
                DeviceNo = "DeviceNo_" + GetRand(),
                BatchNumber = "BatchNumber_" + GetRand(),
                LoggerId = Guid.NewGuid(),
                DurationInMins = GetRandInt(),
                Stage = "WareHouse",
                TemperatureRange = firstTempRange + " - " + secondTempRange,
                RecordDateTime = DateTime.Now.AddDays(-1 * GetRandInt()),
            };
            return batch;
        }
    }
}