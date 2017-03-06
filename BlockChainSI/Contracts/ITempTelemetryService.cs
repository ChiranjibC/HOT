using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Contracts
{
    public interface ITempTelemetryService
    {
        TemparatureTelemetryListViewModel GetTelemetryHistory(int pageSize, int pageNo, string batchCode = "");
        TemparatureTelemetryViewModel Get();
        string Update(TemparatureTelemetryViewModel tempTelemetry);
        string BulkUpdate(DataSet tempTelemetryBulkData, string batchCode);
    }
}
