using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using BlockChainSI.Models;

namespace BlockChainSI.Mock
{
    public class MockTempLogger : MockData, ITempLogger
    {
        private static List<TempLoggerViewModel> _deviceList = null;
        public static List<TempLoggerViewModel> deviceList
        {
            get
            {
                if (_deviceList == null || _deviceList.Count == 0)
                {
                    _deviceList = GetDeviceList(20);
                }
                return _deviceList;
            }
        }
        public IEnumerable<TempLoggerViewModel> GetDeviceList(int pageSize, int pageNo)
        {
            return deviceList;
        }

        public TempLoggerViewModel UpdateDevice(TempLoggerViewModel device)
        {
            device.TempLoggerId = Guid.NewGuid();
            return device;
        }
        
        public TempLoggerViewModel GetDetails(Guid id)
        {
            return deviceList.Find(x => x.TempLoggerId == id);
        }

        private static List<TempLoggerViewModel> GetDeviceList(int count)
        {
            var deviceLists = new List<TempLoggerViewModel>();
            for (int i = 0; i < count; i++)
            {
                deviceLists.Add(GetDevice());
            }
            return deviceLists;
        }

        private static TempLoggerViewModel GetDevice()
        {
            return new TempLoggerViewModel()
            {
                Description = "Temp Logger details_" + GetRand(),
                //DeviceFamily = GetDeviceFamily(),
                TempLoggerId = Guid.NewGuid(),
                TempLoggerShortName = "Temp Logger Name_" + GetRand(),
                TempLoggerNo = "Temp LoggerNo_" + GetRand(),
                LogInterval = 15,
            };
        }
    }
}
