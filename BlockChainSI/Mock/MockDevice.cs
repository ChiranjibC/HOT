using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using BlockChainSI.Models;

namespace BlockChainSI.Mock
{
    public class MockDevice : MockData, IDevice
    {
        public IEnumerable<DeviceViewModel> GetDeviceList(int pageSize, int pageNo)
        {
            return GetDeviceList(pageSize);
        }

        public DeviceViewModel UpdateDevice(DeviceViewModel device)
        {
            device.DeviceId = Guid.NewGuid();
            return device;
        }
        private IEnumerable<DeviceViewModel> GetDeviceList(int count)
        {
            var deviceLists = new List<DeviceViewModel>();
            for (int i = 0; i < count; i++)
            {
                deviceLists.Add(GetDevice());
            }
            return deviceLists;
        }

        private DeviceViewModel GetDevice()
        {
            return new DeviceViewModel()
            {
                Description = "Device details_" + GetRand(),
                DeviceFamily = GetDeviceFamily(),
                DeviceId = Guid.NewGuid(),
                DeviceName = "DeviceName_" + GetRand(),
                DeviceNo = "DeviceNo_" + GetRand(),
                LogInterval = 15,
            };
        }

        private DeviceFamilyViewModel GetDeviceFamily()
        {
            return new DeviceFamilyViewModel()
            {
                DeviceFamilyDesc = "Max_Inn_N_v1.34",
                DeviceFamilyName = "MSTENT",
                DeviceFamilyId = Guid.NewGuid(),
                DeviceFamilyNo = "Max_Inn_N_v1.34",
            };
        }        

    }
}
