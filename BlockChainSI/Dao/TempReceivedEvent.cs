using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Dao
{
    public class TempReceivedEvent
    {
        //event TempReceived(bytes32 _batchId,int16 _temp);

        [Parameter("bytes32", "_batchId", 1)]
        public byte[] BatchId { get; set; }

        [Parameter("int16", "_temp", 2)]
        public int Temp { get; set; }
    }
}
