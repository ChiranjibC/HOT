using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;

namespace BlockChainSI.Services
{
    public class OwnerService : BaseService
    {
        private readonly Web3 web3;        
        
        private Contract contract;
        public OwnerService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(OWNER_ABI, address);
        }

        public async Task<bool> IsValidOwner(string _ownerId)
        {
            try
            {
                var batchFunction = contract.GetFunction("isValidOwner");
                var result = await batchFunction.CallAsync<bool>(_ownerId).ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }

}