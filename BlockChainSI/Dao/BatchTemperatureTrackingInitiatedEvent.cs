using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Dao
{
    public class BatchTemperatureTrackingInitiatedEvent
    {
        //event BatchTemperatureTrackingInitiated(bytes32 _batchId);

        [Parameter("bytes32", "_batchId", 1)]
        public byte[] BatchId { get; set; }
    }
}
