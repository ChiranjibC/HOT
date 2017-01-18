using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockChainSI.Dao
{
    [FunctionOutput]
    public class TemperaturHistoryDao
    {
        public TemperaturHistoryDao()
        {
            ListTime = new uint[100];
            ListTemperature = new short[100];
        }

        [Parameter("uint[]", "_lstTime", 1)]
        public uint[] ListTime { get; set; }

        [Parameter("int16[]", "_lstTemperature", 2)]
        public short[] ListTemperature { get; set; }
    }
}