using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Contracts
{
    public interface IDevice
    {
        IEnumerable<DeviceViewModel> GetDeviceList(int pageSize, int pageNo);
        DeviceViewModel UpdateDevice(DeviceViewModel device);
    }
}
