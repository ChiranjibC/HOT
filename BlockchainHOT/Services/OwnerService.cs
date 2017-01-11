using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Nethereum.Hex.HexTypes;
using Nethereum.Web.Sample.Model.Dao;
using Nethereum.Web3;
using BlockchainHOT.Models.Dao;

namespace Nethereum.Web.Sample.Services
{
    public class OwnerService
    {
        private readonly Web3.Web3 web3;        
        private string owner_abi = @"[{""constant"":false,""inputs"":[{""name"":""_ownerId"",""type"":""address""}],""name"":""addValidOwners"",""outputs"":[],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_ownerId"",""type"":""address""}],""name"":""isValidOwner"",""outputs"":[{""name"":""_isValidOwner"",""type"":""bool""}],""payable"":false,""type"":""function""},{""inputs"":[],""payable"":false,""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""_ownerId"",""type"":""address""},{""indexed"":false,""name"":""_action"",""type"":""bytes32""},{""indexed"":false,""name"":""_addrSender"",""type"":""address""},{""indexed"":false,""name"":""_time"",""type"":""uint256""},{""indexed"":false,""name"":""_info"",""type"":""bytes""}],""name"":""Error"",""type"":""event""}]";
        private Contract contract;
        public OwnerService(Web3.Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(owner_abi, address);
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