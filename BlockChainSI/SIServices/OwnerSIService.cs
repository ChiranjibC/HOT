using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;

namespace BlockChainSI.SIServices
{
    /// <summary>
    /// Owner System Interface service to connect to Block Chain
    /// </summary>
    public class OwnerSIService : BaseSIService
    {
        private readonly Web3 web3;        
        
        private Contract contract;
        public OwnerSIService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(OWNER_ABI, address);
        }

        /// <summary>
        /// This method is used to check if the given address is already registered
        /// in block chain as valid owner (used for initial debugging purpose, as only registered 
        /// owner address can be used as Manufacturer, Shipper, Site, Logger etc.) (No UI as of now).
        /// </summary>
        /// <param name="_ownerId"></param>
        /// <returns></returns>
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