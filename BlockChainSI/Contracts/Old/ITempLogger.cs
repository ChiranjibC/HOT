using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Contracts
{
    public interface ITempLogger
    {
        IEnumerable<TempLoggerViewModel> GetDeviceList(int pageSize, int pageNo);
        TempLoggerViewModel UpdateDevice(TempLoggerViewModel device);
        TempLoggerViewModel GetDetails(Guid id);
    }
}
