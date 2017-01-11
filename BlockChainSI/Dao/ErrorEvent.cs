using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Dao
{
    public class ErrorEvent
    {
        //bytes32 _batchId, bytes32 _action, address _addrSender,uint _time, bytes _info

        [Parameter("bytes32", "_batchId", 1)]
        public byte[] BatchId { get; set; }

        [Parameter("bytes32", "_action", 2)]
        public byte[] Action { get; set; }

        [Parameter("address", "_addrSender", 3)]
        public string AddrSender { get; set; }

        [Parameter("uint", "_time", 4)]
        public uint Time { get; set; }

        [Parameter("bytes", "_info", 5)]
        public byte[] Info { get; set; }
    }
}
