using System.Numerics;
using System;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.ABI;

namespace BlockChainSI.Dao
{
    //[FunctionOutput]
    public class TempTelemetryInput
    {
        public string SenderAddress { get; set; }

        [Parameter("bytes32", 1)]
        public byte[] _batchId { get; set; }

        [Parameter("int16", 2)]
        public short _temp { get; set; }

    }
}