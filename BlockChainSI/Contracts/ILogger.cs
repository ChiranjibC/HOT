using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Contracts
{
    public interface ILogger
    {
        IEnumerable<LoggerViewModel> GetLogList(int pageSize, int pageNo);
        LoggerViewModel UpdateLogItem(LoggerViewModel logItem);
        bool UpdateLogList(IList<LoggerViewModel> logList);
    }
}
