using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockchainHOT.Models.Dao
{
    [FunctionOutput]
    public class Batch
    {
        [Parameter("address")]
        public string _deviceId { get; set; }

        [Parameter("address")]
        public string _currentOwner { get; set; }

        [Parameter("address")]
        public string _producer { get; set; }

        [Parameter("bool")]
        public bool _expiryStatus { get; set; }
    }
}