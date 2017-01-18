using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainSI.Dao
{
    public class OwnershipAcknowledgedEvent
    {
        //event OwnershipAcknowledged(bytes32 _batchId,address _ownerId);

        [Parameter("bytes32", "_batchId", 1)]
        public byte[] BatchId { get; set; }        

        [Parameter("address", "_ownerId", 2)]
        public string OwnerAddr { get; set; }        
    }
}
