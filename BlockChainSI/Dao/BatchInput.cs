using System.Numerics;
using System;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.ABI;

namespace BlockChainSI.Dao
{
    //[FunctionOutput]
    public class BatchInput
    {
        public long Index { get; set; }

        public string SenderAddress { get; set; }

        [Parameter("bytes32", 1)]
        public byte[] _batchId { get; set; }


        //[Parameter("string", 1)]
        //public string _batchId { get; set; }

        [Parameter("address", 2)]
        public string _deviceId { get; set; }

        [Parameter("int16[]", 3)]
        public short[] _lstMinTemp { get; set; }

        [Parameter("int16[]", 4)]
        public short[] _lstMaxTemp { get; set; }

        [Parameter("int16[]", 5)]
        public short[] _lstExpireTickCount { get; set; }

    }
}