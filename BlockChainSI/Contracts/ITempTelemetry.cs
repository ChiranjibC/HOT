using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Contracts
{
    public interface ITempTelemetry
    {
        TemparatureTelemetryListViewModel GetTelemetryHistory(int pageSize, int pageNo, string batchCode = "");
        TemparatureTelemetryViewModel Get();
        string Update(TemparatureTelemetryViewModel tempTelemetry);
    }
}
