﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Nethereum.Hex.HexTypes;
using Nethereum.Web.Sample.Model.Dao;
using Nethereum.Web3;
using BlockchainHOT.Models.Dao;
using Nethereum.ABI.Encoders;
using System.Threading;

namespace Nethereum.Web.Sample.Services
{
    public class BatchService
    {
        private readonly Web3.Web3 web3;
        //private string abi = @"[{ ""constant"": true, ""inputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""name"": ""proposals"", ""outputs"": [{ ""name"": ""recipient"", ""type"": ""address"" }, { ""name"": ""amount"", ""type"": ""uint256"" }, { ""name"": ""description"", ""type"": ""string"" }, { ""name"": ""votingDeadline"", ""type"": ""uint256"" }, { ""name"": ""open"", ""type"": ""bool"" }, { ""name"": ""proposalPassed"", ""type"": ""bool"" }, { ""name"": ""proposalHash"", ""type"": ""bytes32"" }, { ""name"": ""proposalDeposit"", ""type"": ""uint256"" }, { ""name"": ""newCurator"", ""type"": ""bool"" }, { ""name"": ""yea"", ""type"": ""uint256"" }, { ""name"": ""nay"", ""type"": ""uint256"" }, { ""name"": ""creator"", ""type"": ""address"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_spender"", ""type"": ""address"" }, { ""name"": ""_amount"", ""type"": ""uint256"" }], ""name"": ""approve"", ""outputs"": [{ ""name"": ""success"", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""minTokensToCreate"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""rewardAccount"", ""outputs"": [{ ""name"": """", ""type"": ""address"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""daoCreator"", ""outputs"": [{ ""name"": """", ""type"": ""address"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""totalSupply"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""divisor"", ""outputs"": [{ ""name"": ""divisor"", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""extraBalance"", ""outputs"": [{ ""name"": """", ""type"": ""address"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_proposalID"", ""type"": ""uint256"" }, { ""name"": ""_transactionData"", ""type"": ""bytes"" }], ""name"": ""executeProposal"", ""outputs"": [{ ""name"": ""_success"", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_from"", ""type"": ""address"" }, { ""name"": ""_to"", ""type"": ""address"" }, { ""name"": ""_value"", ""type"": ""uint256"" }], ""name"": ""transferFrom"", ""outputs"": [{ ""name"": ""success"", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [], ""name"": ""unblockMe"", ""outputs"": [{ ""name"": """", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""totalRewardToken"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""actualBalance"", ""outputs"": [{ ""name"": ""_actualBalance"", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""closingTime"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": """", ""type"": ""address"" }], ""name"": ""allowedRecipients"", ""outputs"": [{ ""name"": """", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_to"", ""type"": ""address"" }, { ""name"": ""_value"", ""type"": ""uint256"" }], ""name"": ""transferWithoutReward"", ""outputs"": [{ ""name"": ""success"", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [], ""name"": ""refund"", ""outputs"": [], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_recipient"", ""type"": ""address"" }, { ""name"": ""_amount"", ""type"": ""uint256"" }, { ""name"": ""_description"", ""type"": ""string"" }, { ""name"": ""_transactionData"", ""type"": ""bytes"" }, { ""name"": ""_debatingPeriod"", ""type"": ""uint256"" }, { ""name"": ""_newCurator"", ""type"": ""bool"" }], ""name"": ""newProposal"", ""outputs"": [{ ""name"": ""_proposalID"", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": """", ""type"": ""address"" }], ""name"": ""DAOpaidOut"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""minQuorumDivisor"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_newContract"", ""type"": ""address"" }], ""name"": ""newContract"", ""outputs"": [], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": ""_owner"", ""type"": ""address"" }], ""name"": ""balanceOf"", ""outputs"": [{ ""name"": ""balance"", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_recipient"", ""type"": ""address"" }, { ""name"": ""_allowed"", ""type"": ""bool"" }], ""name"": ""changeAllowedRecipients"", ""outputs"": [{ ""name"": ""_success"", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [], ""name"": ""halveMinQuorum"", ""outputs"": [{ ""name"": ""_success"", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": """", ""type"": ""address"" }], ""name"": ""paidOut"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_proposalID"", ""type"": ""uint256"" }, { ""name"": ""_newCurator"", ""type"": ""address"" }], ""name"": ""splitDAO"", ""outputs"": [{ ""name"": ""_success"", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""DAOrewardAccount"", ""outputs"": [{ ""name"": """", ""type"": ""address"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""proposalDeposit"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""numberOfProposals"", ""outputs"": [{ ""name"": ""_numberOfProposals"", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""lastTimeMinQuorumMet"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_toMembers"", ""type"": ""bool"" }], ""name"": ""retrieveDAOReward"", ""outputs"": [{ ""name"": ""_success"", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [], ""name"": ""receiveEther"", ""outputs"": [{ ""name"": """", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_to"", ""type"": ""address"" }, { ""name"": ""_value"", ""type"": ""uint256"" }], ""name"": ""transfer"", ""outputs"": [{ ""name"": ""success"", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""isFueled"", ""outputs"": [{ ""name"": """", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_tokenHolder"", ""type"": ""address"" }], ""name"": ""createTokenProxy"", ""outputs"": [{ ""name"": ""success"", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": ""_proposalID"", ""type"": ""uint256"" }], ""name"": ""getNewDAOAddress"", ""outputs"": [{ ""name"": ""_newDAO"", ""type"": ""address"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_proposalID"", ""type"": ""uint256"" }, { ""name"": ""_supportsProposal"", ""type"": ""bool"" }], ""name"": ""vote"", ""outputs"": [{ ""name"": ""_voteID"", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [], ""name"": ""getMyReward"", ""outputs"": [{ ""name"": ""_success"", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": """", ""type"": ""address"" }], ""name"": ""rewardToken"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_from"", ""type"": ""address"" }, { ""name"": ""_to"", ""type"": ""address"" }, { ""name"": ""_value"", ""type"": ""uint256"" }], ""name"": ""transferFromWithoutReward"", ""outputs"": [{ ""name"": ""success"", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": ""_owner"", ""type"": ""address"" }, { ""name"": ""_spender"", ""type"": ""address"" }], ""name"": ""allowance"", ""outputs"": [{ ""name"": ""remaining"", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": false, ""inputs"": [{ ""name"": ""_proposalDeposit"", ""type"": ""uint256"" }], ""name"": ""changeProposalDeposit"", ""outputs"": [], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": """", ""type"": ""address"" }], ""name"": ""blocked"", ""outputs"": [{ ""name"": """", ""type"": ""uint256"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""curator"", ""outputs"": [{ ""name"": """", ""type"": ""address"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [{ ""name"": ""_proposalID"", ""type"": ""uint256"" }, { ""name"": ""_recipient"", ""type"": ""address"" }, { ""name"": ""_amount"", ""type"": ""uint256"" }, { ""name"": ""_transactionData"", ""type"": ""bytes"" }], ""name"": ""checkProposalCode"", ""outputs"": [{ ""name"": ""_codeChecksOut"", ""type"": ""bool"" }], ""type"": ""function"" }, { ""constant"": true, ""inputs"": [], ""name"": ""privateCreation"", ""outputs"": [{ ""name"": """", ""type"": ""address"" }], ""type"": ""function"" }, { ""inputs"": [{ ""name"": ""_curator"", ""type"": ""address"" }, { ""name"": ""_daoCreator"", ""type"": ""address"" }, { ""name"": ""_proposalDeposit"", ""type"": ""uint256"" }, { ""name"": ""_minTokensToCreate"", ""type"": ""uint256"" }, { ""name"": ""_closingTime"", ""type"": ""uint256"" }, { ""name"": ""_privateCreation"", ""type"": ""address"" }], ""type"": ""constructor"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""_from"", ""type"": ""address"" }, { ""indexed"": true, ""name"": ""_to"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""_amount"", ""type"": ""uint256"" }], ""name"": ""Transfer"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""_owner"", ""type"": ""address"" }, { ""indexed"": true, ""name"": ""_spender"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""_amount"", ""type"": ""uint256"" }], ""name"": ""Approval"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": false, ""name"": ""value"", ""type"": ""uint256"" }], ""name"": ""FuelingToDate"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""to"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""amount"", ""type"": ""uint256"" }], ""name"": ""CreatedToken"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""to"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""value"", ""type"": ""uint256"" }], ""name"": ""Refund"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""proposalID"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""recipient"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""amount"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""newCurator"", ""type"": ""bool"" }, { ""indexed"": false, ""name"": ""description"", ""type"": ""string"" }], ""name"": ""ProposalAdded"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""proposalID"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""position"", ""type"": ""bool"" }, { ""indexed"": true, ""name"": ""voter"", ""type"": ""address"" }], ""name"": ""Voted"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""proposalID"", ""type"": ""uint256"" }, { ""indexed"": false, ""name"": ""result"", ""type"": ""bool"" }, { ""indexed"": false, ""name"": ""quorum"", ""type"": ""uint256"" }], ""name"": ""ProposalTallied"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""_newCurator"", ""type"": ""address"" }], ""name"": ""NewCurator"", ""type"": ""event"" }, { ""anonymous"": false, ""inputs"": [{ ""indexed"": true, ""name"": ""_recipient"", ""type"": ""address"" }, { ""indexed"": false, ""name"": ""_allowed"", ""type"": ""bool"" }], ""name"": ""AllowedRecipientChanged"", ""type"": ""event"" }]";
        private string batch_abi = @"[{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""},{""name"":""_deviceId"",""type"":""address""},{""name"":""_lstMinTemp"",""type"":""int16[]""},{""name"":""_lstMaxTemp"",""type"":""int16[]""},{""name"":""_lstExpireTickCount"",""type"":""int16[]""}],""name"":""initiateBatchTracking"",""outputs"":[],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""}],""name"":""acknowledgeReceive"",""outputs"":[],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""},{""name"":""_tempRangeId"",""type"":""uint8""},{""name"":""_tickCount"",""type"":""int16""}],""name"":""updateBatchExpiryStatus"",""outputs"":[],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""}],""name"":""isValidBatch"",""outputs"":[{""name"":""_isValidBatch"",""type"":""bool""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""}],""name"":""getBatchInfo"",""outputs"":[{""name"":""_deviceId"",""type"":""address""},{""name"":""_currentOwner"",""type"":""address""},{""name"":""_producer"",""type"":""address""},{""name"":""_expiryStatus"",""type"":""bool""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""}],""name"":""getAllowedBatchTempRanges"",""outputs"":[{""name"":""_lstRangeId"",""type"":""uint8[]""},{""name"":""_lstMinTemp"",""type"":""int16[]""},{""name"":""_lstMaxTemp"",""type"":""int16[]""},{""name"":""_lstExpireTickCount"",""type"":""int16[]""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""}],""name"":""getBatchOwnershipHistory"",""outputs"":[{""name"":""_lstOwner"",""type"":""address[]""},{""name"":""_lstStartTime"",""type"":""uint256[]""},{""name"":""_lstEndTime"",""type"":""uint256[]""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_addrOwnerManagerCtr"",""type"":""address""}],""name"":""setAddrOwnerManager"",""outputs"":[],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""},{""name"":""_temp"",""type"":""int16""}],""name"":""getTempRangeIdByTemp"",""outputs"":[{""name"":""_tempRangeId"",""type"":""uint8""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""}],""name"":""getAssociatedDeviceId"",""outputs"":[{""name"":""_deviceId"",""type"":""address""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""},{""name"":""_rangeId"",""type"":""uint8""}],""name"":""getMinMaxTempByRangeId"",""outputs"":[{""name"":""_minTemp"",""type"":""int16""},{""name"":""_maxTemp"",""type"":""int16""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""},{""name"":""_deviceId"",""type"":""address""}],""name"":""associateDevice"",""outputs"":[],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_addrTempManagerCtr"",""type"":""address""}],""name"":""setAddrTempManager"",""outputs"":[],""payable"":false,""type"":""function""},{""inputs"":[],""payable"":false,""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""_batchId"",""type"":""bytes32""},{""indexed"":false,""name"":""_action"",""type"":""bytes32""},{""indexed"":false,""name"":""_addrSender"",""type"":""address""},{""indexed"":false,""name"":""_time"",""type"":""uint256""},{""indexed"":false,""name"":""_info"",""type"":""bytes""}],""name"":""Error"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""_batchId"",""type"":""bytes32""}],""name"":""BatchTemperatureTrackingInitiated"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""_batchId"",""type"":""bytes32""},{""indexed"":false,""name"":""_deviceId"",""type"":""address""}],""name"":""AssociateDeviceIdModified"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""_batchId"",""type"":""bytes32""},{""indexed"":false,""name"":""_ownerId"",""type"":""address""}],""name"":""OwnershipAcknowledged"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""_batchId"",""type"":""bytes32""}],""name"":""BatchExpired"",""type"":""event""}]";
        private string ADMIN_PWD = "Password-123";
        private string SenderAddress = "0xe22a9544599524ea274fc78a7c998202c243930c";
        private Contract contract;
        private Event ethEvent;
        public BatchService()
        { }
        public BatchService(Web3.Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(batch_abi, address);
            this.ethEvent = contract.GetEvent("BatchTemperatureTrackingInitiated");
        }

        //public async Task<bool> InitiateBatchTracking(CreateBatchInput batch)
        //{
        //    try
        //    {
        //        var batchFunction = contract.GetFunction("initiateBatchTracking");
        //        var result = await batchFunction.CallAsync<bool>(batch._batchId, batch._deviceId, batch._lstMinTemp, batch._lstMaxTemp, batch._lstExpireTickCount).ConfigureAwait(false);
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }            
        //}


        public async Task<Batch> InitiateBatchTracking(CreateBatchInput _newBatch)
        {
            var web3Srv = new Web3.Web3("http://echaind23.centralus.cloudapp.azure.com:8545");
            var senderAddressSrv = "0xe22a9544599524ea274fc78a7c998202c243930c";
            var passwordSrv = "Password-123";
            var unlockAccountResultSrv =
                await web3Srv.Personal.UnlockAccount.SendRequestAsync(senderAddressSrv, passwordSrv, new HexBigInteger(120));
            /////////////////////////////////////////////////
            //var newAcc = await web3Srv.Personal.NewAccount.SendRequestAsync("Password-123");
            //step-2: Got to the portal and fund
            //
            /////////////////////////////////////////////////
            var balance = await web3Srv.Eth.GetBalance.SendRequestAsync(senderAddressSrv);
            var abi_batch = @"[{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""},{""name"":""_deviceId"",""type"":""address""},{""name"":""_lstMinTemp"",""type"":""int16[]""},{""name"":""_lstMaxTemp"",""type"":""int16[]""},{""name"":""_lstExpireTickCount"",""type"":""int16[]""}],""name"":""initiateBatchTracking"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""}],""name"":""acknowledgeReceive"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""},{""name"":""_tempRangeId"",""type"":""uint8""},{""name"":""_tickCount"",""type"":""int16""}],""name"":""updateBatchExpiryStatus"",""outputs"":[],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""}],""name"":""isValidBatch"",""outputs"":[{""name"":""_isValidBatch"",""type"":""bool""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""}],""name"":""getBatchInfo"",""outputs"":[{""name"":""_deviceId"",""type"":""address""},{""name"":""_currentOwner"",""type"":""address""},{""name"":""_producer"",""type"":""address""},{""name"":""_expiryStatus"",""type"":""bool""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""}],""name"":""getAllowedBatchTempRanges"",""outputs"":[{""name"":""_lstRangeId"",""type"":""uint8[]""},{""name"":""_lstMinTemp"",""type"":""int16[]""},{""name"":""_lstMaxTemp"",""type"":""int16[]""},{""name"":""_lstExpireTickCount"",""type"":""int16[]""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""}],""name"":""getBatchOwnershipHistory"",""outputs"":[{""name"":""_lstOwner"",""type"":""address[]""},{""name"":""_lstStartTime"",""type"":""uint256[]""},{""name"":""_lstEndTime"",""type"":""uint256[]""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_addrOwnerManagerCtr"",""type"":""address""}],""name"":""setAddrOwnerManager"",""outputs"":[],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""},{""name"":""_temp"",""type"":""int16""}],""name"":""getTempRangeIdByTemp"",""outputs"":[{""name"":""_tempRangeId"",""type"":""uint8""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""}],""name"":""getAssociatedDeviceId"",""outputs"":[{""name"":""_deviceId"",""type"":""address""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""},{""name"":""_rangeId"",""type"":""uint8""}],""name"":""getMinMaxTempByRangeId"",""outputs"":[{""name"":""_minTemp"",""type"":""int16""},{""name"":""_maxTemp"",""type"":""int16""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_batchId"",""type"":""bytes32""},{""name"":""_deviceId"",""type"":""address""}],""name"":""associateDevice"",""outputs"":[{""name"":"""",""type"":""bool""}],""payable"":false,""type"":""function""},{""constant"":false,""inputs"":[{""name"":""_addrTempManagerCtr"",""type"":""address""}],""name"":""setAddrTempManager"",""outputs"":[],""payable"":false,""type"":""function""},{""inputs"":[],""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""_batchId"",""type"":""bytes32""},{""indexed"":false,""name"":""_action"",""type"":""bytes32""},{""indexed"":false,""name"":""_addrSender"",""type"":""address""},{""indexed"":false,""name"":""_time"",""type"":""uint256""},{""indexed"":false,""name"":""_info"",""type"":""bytes""}],""name"":""Error"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""_batchId"",""type"":""bytes32""}],""name"":""BatchTemperatureTrackingInitiated"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""_batchId"",""type"":""bytes32""},{""indexed"":false,""name"":""_deviceId"",""type"":""address""}],""name"":""AssociateDeviceIdModified"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""_batchId"",""type"":""bytes32""},{""indexed"":false,""name"":""_ownerId"",""type"":""address""}],""name"":""OwnershipAcknowledged"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""name"":""_batchId"",""type"":""bytes32""}],""name"":""BatchExpired"",""type"":""event""}]";
            var byteCodeBatch =
                "0x60606040526002805460a060020a61ffff02191675d8f100000000000000000000000000000000000000001760b060020a61ffff02191677270f000000000000000000000000000000000000000000001760c060020a61ffff02191679270f00000000000000000000000000000000000000000000000017905560008054600160a060020a0319166c0100000000000000000000000033810204178155611af99081906100ab90396000f3606060405236156100a35760e060020a60003504633060a12881146100a85780634ce93a72146101945780634eb83288146101ac578063692d6e0814610378578063753207c71461038857806380396869146103a657806393ff83811461041c5780639712e4981461047b57806398d4d1ab146104a15780639c16bcfd146105db578063a6a2c39614610606578063b2569a2314610621578063e304258b1461063a575b610002565b346100025760408051604435600481810135602081810285810182019096528185526106609583359560248035966064959294910192829185019084908082843750506040805196358089013560208181028a81018201909452818a529799986084989097506024929092019550935083925085019084908082843750506040805196358089013560208181028a81018201909452818a5297999860a498909750602492909201955093508392508501908490808284375094965050505050505060006000610940875b600081815260036020526040812060010154600160a060020a0316115b919050565b346100025761066060043560006000610ee983610172565b346100025761067460043560243560443560025460009033600160a060020a0390811691161461026757604080518581527f75706461746542617463684578706972795374617475730000000000000000006020820152600160a060020a0333168183015242606082015260a060808201819052600e908201527f4163636573732064656e6965642100000000000000000000000000000000000060c08201529051600080516020611ab98339815191529181900360e00190a15b60008481526003602052604090206004015460a060020a900460ff16151561037257600084815260036020819052604090912001805460ff6000198601169081101561000257600091825260209091200154650100000000009004600190810b915081810b9083900b1380156103045750600254600182810b7801000000000000000000000000000000000000000000000000909204810b900b14155b1561037257600084815260036020908152604091829020600401805474ff0000000000000000000000000000000000000000191660a060020a179055815186815291517f11b5ee16b122758ece1863a4d0ea2c72bb1ec6b4eb58b1461d8219f9ada496c99281900390910190a15b50505050565b3461000257610660600435610172565b34610002576106766004356000600060006000600061117186610172565b346100025760408051602080820183526000808352835180830185528181528451808401865282815285518085018752838152865180860188528481528751808701895285815288518088018a5286815289519788019099528587526106ab98600435989795969495939493846112148b610172565b3461000257604080516020808201835260008083528351808301855281815284518084018652828152855180850187528381528651808601885284815287519586019097528385526107a696600435969593949293836114ba89610172565b346100025761067460043560005433600160a060020a039081169116146116c257610002565b34610002576108666004356024356000828152600360208190526040822001815b815460ff821610156116e157818160ff168154811015610002576000918252602090912001546101009004600190810b810b9085900b12158061053b5750600260149054906101000a900460010b60010b828260ff168154811015610002576000918252602090912001546101009004600190810b900b145b80156105b25750600260169054906101000a900460010b60010b828260ff1681548110156100025760009182526020909120015463010000009004600190810b900b14806105b25750818160ff1681548110156100025760009182526020909120015463010000009004600190810b810b9085900b125b156117a157818160ff1681548110156100025760009182526020909120015460ff169250611799565b346100025761087c600435600081815260036020526040902060040154600160a060020a031661018f565b34610002576108986004356024356000600061182d84610172565b3461000257610660600435602435600061193083610172565b346100025761067460043560005433600160a060020a03908116911614611a9a57610002565b604080519115158252519081900360200190f35b005b60408051600160a060020a03958616815293851660208501529190931682820152911515606082015290519081900360800190f35b60405180806020018060200180602001806020018581038552898181518152602001915080519060200190602002808383829060006004602084601f0104600302600f01f1509050018581038452888181518152602001915080519060200190602002808383829060006004602084601f0104600302600f01f1509050018581038352878181518152602001915080519060200190602002808383829060006004602084601f0104600302600f01f1509050018581038252868181518152602001915080519060200190602002808383829060006004602084601f0104600302600f01f1509050019850505050505050505060405180910390f35b604051808060200180602001806020018481038452878181518152602001915080519060200190602002808383829060006004602084601f0104600302600f01f1509050018481038352868181518152602001915080519060200190602002808383829060006004602084601f0104600302600f01f1509050018481038252858181518152602001915080519060200190602002808383829060006004602084601f0104600302600f01f150905001965050505050505060405180910390f35b6040805160ff9092168252519081900360200190f35b60408051600160a060020a039092168252519081900360200190f35b604051808360010b81526020018260010b81526020019250505060405180910390f35b6000878152600360209081526040918290206004018054600160a060020a031916606060020a8a8102041774ff000000000000000000000000000000000000000019169055815189815291517f16b52089c398e403f6d5d6a334cddd3ac0322b9a443fe209b7f994f2c8858a8e9281900390910190a1600191505b5095945050505050565b156109da57604080518881527f496e6974696174654261746368547261636b696e6700000000000000000000006020820152600160a060020a0333168183015242606082015260a060808201819052601e908201527f4475706c69636174652042617463684964206e6f7420616c6c6f77656421000060c08201529051600080516020611ab98339815191529181900360e00190a1610936565b845115806109f65750835185511415806109f657508251855114155b15610a9057604080518881527f496e6974696174654261746368547261636b696e6700000000000000000000006020820152600160a060020a0333168183015242606082015260a0608082018190526019908201527f496e76616c696420696e70757420706172616d6574657273210000000000000060c08201529051600080516020611ab98339815191529181900360e00190a1610936565b60015460048054600160a060020a031916606060020a600160a060020a03938416810204178082556040805160006020918201819052825160e060020a63b8248dff028152338716958101959095529151929094169363b8248dff936024808201949293918390030190829087803b156100025760325a03f1156100025750506040515115159050610bd857604080518881527f496e6974696174654261746368547261636b696e6700000000000000000000006020820152600160a060020a0333168183015242606082015260a060808201819052603d908201527f4163636573732064656e696564202d206f776e657220646f6573206e6f74206260c08201527f656c6f6e677320746f2076616c6964206f776e6572732067726f75702100000060e08201529051600080516020611ab9833981519152918190036101000190a1610936565b60008781526003602052604090208054600181018083558281838015829011610c4257600302816003028360005260206000209182019101610c4291905b80821115610d2b578054600160a060020a03191681556000600182018190556002820155600301610c16565b505050600092835250602080832060408051606081018252338082524282860181905291830187905260039586029093018054600160a060020a0319908116606060020a95860295909504948517825560018083019390935560029182018890558e885295909452908520928301805485168317905591909101805490921617905590505b84518160ff1610156108bb576000878152600360208190526040909120018054600181018083558281838015829011610d2f57600083815260209020610d2f9181019083015b80821115610d2b57805466ffffffffffffff19168155600101610d0d565b5090565b505050919090600052602060002090016000608060405190810160405280856001018152602001898660ff16815181101561000257906020019060200201518152602001888660ff16815181101561000257906020019060200201518152602001878660ff168151811015610002576020908102919091018101519091528151845491830151604084015160609094015160ff1990931660f860020a928302929092049190911762ffff00191661010060f060020a928302839004021764ffff00000019166301000000938202829004939093029290921766ffff00000000001916650100000000009183029290920402179091555050600101610cc7565b50505091909060005260206000209060030201600050604080516060810182523380825242602080840182905260009385018490528554606060020a80850204600160a060020a03199182168117885560018801939093556002968701949094559487018054909316179091558151878152600160a060020a039091169281019290925280517f7e7139e743e9ed734e621cad8af8f14d1af73c4bd726e26d8ad14392a206e8a39350918290030190a1600191505b50919050565b1515610f7257604080518481527f41636b6e6f776c656467655265636569766500000000000000000000000000006020820152600160a060020a0333168183015242606082015260a060808201819052601190820152600080516020611ad983398151915260c08201529051600080516020611ab98339815191529181900360e00190a1610ee3565b60015460048054600160a060020a031916606060020a600160a060020a03938416810204178082556040805160006020918201819052825160e060020a63b8248dff028152338716958101959095529151929094169363b8248dff936024808201949293918390030190829087803b156100025760325a03f11561000257505060405151151590506110ba57604080518481527f41636b6e6f776c656467655265636569766500000000000000000000000000006020820152600160a060020a0333168183015242606082015260a060808201819052603d908201527f4163636573732064656e696564202d206f776e657220646f6573206e6f74206260c08201527f656c6f6e677320746f2076616c6964206f776e6572732067726f75702100000060e08201529051600080516020611ab9833981519152918190036101000190a1610ee3565b506000828152600360205260409020805442908290600019810190811015610002579060005260206000209060030201600050600201558054600181018083558291908281838015829011610e2e57600302816003028360005260206000209182019101610e2e9190610c16565b5050506000838152600360205260409020600481015460028201546001830154600160a060020a0380841696509182169450169160a060020a90910460ff16905b509193509193565b151561112857604080518781527f4765744261746368496e666f00000000000000000000000000000000000000006020820152600160a060020a0333168183015242606082015260a060808201819052601190820152600080516020611ad983398151915260c08201529051600080516020611ab98339815191529181900360e00190a1611169565b8484848499509950995099505b5050505050509193509193565b151561129d57604080518c81527f476574416c6c6f776564426174636854656d7052616e676573000000000000006020820152600160a060020a0333168183015242606082015260a060808201819052601190820152600080516020611ad983398151915260c08201529051600080516020611ab98339815191529181900360e00190a1611207565b60008b8152600360208190526040918290209081015491519097508059106112c25750595b9080825280602002602001820160405280156112d9575b506003870154604051919650908059106112f05750595b908082528060200260200182016040528015611307575b5060038701546040519195509080591061131e5750595b908082528060200260200182016040528015611335575b5060038701546040519194509080591061134c5750595b908082528060200260200182016040528015611363575b509150600090505b600386015460ff821610156111fa5760038601805460ff83169081101561000257600091825260209091200154855160ff918216918791908416908110156100025760ff92831660209182029092010152600387018054909183169081101561000257600091825260209091200154845161010090910460010b90859060ff8416908110156100025760019290920b60209283029091019091015260038601805460ff831690811015610002576000918252602090912001548351630100000090910460010b90849060ff8416908110156100025760019290920b60209283029091019091015260038601805460ff8316908110156100025760009182526020909120015482516501000000000090910460010b90839060ff84169081101561000257600192830b60209182029290920101520161136b565b8383839750975097505b50505050509193909250565b151561154357604080518a81527f47657442617463684f776e657273686970486973746f727900000000000000006020820152600160a060020a0333168183015242606082015260a060808201819052601190820152600080516020611ad983398151915260c08201529051600080516020611ab98339815191529181900360e00190a16114ae565b60008981526003602052604090819020805491519096508059106115645750595b90808252806020026020018201604052801561157b575b5085546040519195509080591061158f5750595b9080825280602002602001820160405280156115a6575b508554604051919450908059106115ba5750595b9080825280602002602001820160405280156115d1575b509150600090505b845460ff821610156114a4578454859060ff831690811015610002579060005260206000209060030201600050548451600160a060020a0390911690859060ff84169081101561000257600160a060020a039092166020928302909101909101528454859060ff83169081101561000257906000526020600020906003020160005060010160005054838260ff16815181101561000257602090810290910101528454859060ff83169081101561000257906000526020600020906003020160005060020160005054828260ff16815181101561000257602090810290910101526001016115d9565b60018054606060020a80840204600160a060020a031990911617905550565b604080518681527f47657454656d7052616e67654964427954656d700000000000000000000000006020820152600160a060020a0333168183015242606082015260a060808201819052602a908201527f54656d7052616e6765206e6f7420666f756e6420666f7220676976656e20746560c08201527f6d7065726174757265210000000000000000000000000000000000000000000060e08201529051600080516020611ab9833981519152918190036101000190a15b505092915050565b6001016104c2565b600084815260036020819052604090912001805460ff6000198601169081101561000257906000526020600020900160005054600085815260036020819052604090912001805461010090920460010b9160ff60001987011690811015610002576000918252602090912001549092506301000000900460010b90505b9250929050565b15156117a957604080518581527f6765744d696e4d617854656d70427952616e67654964000000000000000000006020820152600160a060020a0333168183015242606082015260a060808201819052601190820152600080516020611ad983398151915260c08201529051600080516020611ab98339815191529181900360e00190a1611826565b6000838152600360209081526040918290206004018054600160a060020a031916606060020a868102041790558151858152600160a060020a0385169181019190915281517f753c5047acde828a8eb1b899c4262914a3aa28ea111b9bdc5be4474c3443aef9929181900390910190a15060015b92915050565b15156119b957604080518481527f4173736f636961746544657669636500000000000000000000000000000000006020820152600160a060020a0333168183015242606082015260a060808201819052601190820152600080516020611ad983398151915260c08201529051600080516020611ab98339815191529181900360e00190a161192a565b60008381526003602052604090206001015433600160a060020a039081169116146118b657604080518481527f4173736f636961746544657669636500000000000000000000000000000000006020820152600160a060020a0333168183015242606082015260a060808201819052602c908201527f4f6e6c792070726f64756365722063616e206368616e6765206465766963652060c08201527f6173736f63696174696f6e21000000000000000000000000000000000000000060e08201529051600080516020611ab9833981519152918190036101000190a161192a565b60028054606060020a80840204600160a060020a0319909116179055505667d7a77c3d27758405038be0e10b93523da971ac5ff30adef64ae754b8a50944496e76616c696420426174636820496421000000000000000000000000000000";
            var batchManagerAddress = "0x3487c5bef71f8182df1104ddbd3001388843cc52";//"0x3487c5bef71f8182df1104ddbd3001388843cc52";

            balance = await web3Srv.Eth.GetBalance.SendRequestAsync(batchManagerAddress);
            var accounts = await web3Srv.Eth.Accounts.SendRequestAsync();
            var contract = web3Srv.Eth.GetContract(abi_batch, batchManagerAddress);
            var batchFunction = contract.GetFunction("initiateBatchTracking");
            var _EvtBatchTemperatureTrackingInitiated = contract.GetEvent("BatchTemperatureTrackingInitiated");
            
            var resultBatch = await batchFunction.SendTransactionAsync(senderAddressSrv,
                                                                        new HexBigInteger(500000),
                                                                        new HexBigInteger(0),
                                                                        _newBatch._batchId,
                                                                        _newBatch._deviceId,
                                                                        _newBatch._lstMinTemp,
                                                                        _newBatch._lstMaxTemp,
                                                                        _newBatch._lstExpireTickCount);
            var receipt = await web3Srv.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(resultBatch);
            while (receipt == null)
            {
                Thread.Sleep(5000);
                receipt = await web3Srv.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(resultBatch);
            }
            //ITypeEncoder byte32Encoder = new Bytes32TypeEncoder();
            //var batchId = byte32Encoder.Encode("B1121516");
            var batch = await GetBatchInfo(_newBatch._batchId.ToString());
            //var resultBatch1 = await batchFunction.CallAsync<bool>(_newBatch._batchId,
            //                                                            _newBatch._deviceId,
            //                                                            _newBatch._lstMinTemp,
            //                                                            _newBatch._lstMaxTemp,
            //                                                            _newBatch._lstExpireTickCount).ConfigureAwait(true);

            return batch;
        }

        public async Task<bool> InitiateBatchTrackingTemp(CreateBatchInput batch)
        {
            try
            {
                

                var batchFunction = contract.GetFunction("initiateBatchTracking");
                var updatedStatus = await batchFunction.SignAndSendTransactionAsync(ADMIN_PWD, SenderAddress, batch._batchId, batch._deviceId, batch._lstMinTemp, batch._lstMaxTemp, batch._lstExpireTickCount);

                var status = await testInitiateBatch(batch);

                //var privateKey = "4c45ba3387d00578b24c9b9be24b55678b165465934742b4440c930517452f3d";
                //var senderAddress = "0x013CaDa165ED4C86D2607bC9BfD2Cb6869995A56";
                //var batchFunction = contract.GetFunction("initiateBatchTracking");
                //await batchFunction.SendTransactionAsync(senderAddress, batch._batchId, batch._deviceId, batch._lstMinTemp, batch._lstMaxTemp, batch._lstExpireTickCount);
                ////await batchFunction.CallAsync<bool>(batch._batchId, batch._deviceId, batch._lstMinTemp, batch._lstMaxTemp, batch._lstExpireTickCount);
                ////var x = await ethEvent.GetAllChanges(new RPC.Eth.Filters.NewFilterInput());
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private async Task<bool> testInitiateBatch(CreateBatchInput batch)
        {
            var privateKey = "4c45ba3387d00578b24c9b9be24b55678b165465934742b4440c930517452f3d";
            var senderAddress = "0x013CaDa165ED4C86D2607bC9BfD2Cb6869995A56";
            var receiveAddress = "0x0977EE50104A09e37B7E675b847d86C7d131fc91";
            var web3 = new Web3.Web3();
            var txCount = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(senderAddress);
            var encoded = web3.OfflineTransactionSigning.SignTransaction(privateKey, receiveAddress, 10, txCount.Value);
            var txId = await web3.Eth.Transactions.SendRawTransaction.SendRequestAsync("0x" + encoded);

            await web3.Miner.Start.SendRequestAsync(4);
            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(txId);
            while (receipt == null)
            {
                Thread.Sleep(1000);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(txId);
            }
            await web3.Miner.Stop.SendRequestAsync();

            var contractAddress = receipt.ContractAddress;
            var contract = web3.Eth.GetContract(batch_abi, contractAddress);
            var batchFunction = contract.GetFunction("initiateBatchTracking");
            var result = await batchFunction.CallAsync<bool>(batch._batchId, batch._deviceId, batch._lstMinTemp, batch._lstMaxTemp, batch._lstExpireTickCount);

            //var batchFunction = contract.GetFunction("initiateBatchTracking");
            //await batchFunction.SendTransactionAsync(senderAddress, batch._batchId, batch._deviceId, batch._lstMinTemp, batch._lstMaxTemp, batch._lstExpireTickCount);
            return true;
        }


        public async Task<Batch> GetBatchInfo(string batchId)
        {
            try
            {
                ITypeEncoder byte32Encoder = new Bytes32TypeEncoder();
                var batchDetails = contract.GetFunction("getBatchInfo");
                var batch = await batchDetails.CallDeserializingToObjectAsync<Batch>(byte32Encoder.Encode(batchId)).ConfigureAwait(false);
                return batch;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        

        public async Task<long> GetAssociatedDeviceId(string batchId)
        {
            try
            {
                var batchDetails = contract.GetFunction("getAssociatedDeviceId");
                var batch = await batchDetails.CallDeserializingToObjectAsync<long>(batchId).ConfigureAwait(false);
                return batch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<long> GetNumberOfProposals()
        {
            return contract.GetFunction("numberOfProposals").CallAsync<long>();
        }

        public async Task<List<Proposal>> GetAllProposals()
        {

            var numberOfProposals = await GetNumberOfProposals().ConfigureAwait(false);
            var proposals = new List<Proposal>();

            for (var i = 0; i < numberOfProposals; i++)
            {
                proposals.Add(await GetProposal(i).ConfigureAwait(false));
            }
            return proposals;
        }

        public async Task<List<Proposal>> GetLatestProposals(long total)
        {
            var numberOfProposals = await GetNumberOfProposals().ConfigureAwait(false);
            if (total >= numberOfProposals) total = numberOfProposals - 1;
            var proposals = new List<Proposal>();

            for (var i = numberOfProposals - 1; i >= (numberOfProposals - total); i--)
            {
                proposals.Add(await GetProposal(i).ConfigureAwait(false));
            }
            return proposals;
        }

        public async Task<List<Proposal>> GetAllProposals(long startProposal, long lastProposal)
        {
            var numberOfProposals = await GetNumberOfProposals().ConfigureAwait(false);
            if (lastProposal >= numberOfProposals) lastProposal = numberOfProposals - 1;
            var proposals = new List<Proposal>();

            for (var i = startProposal; i <= lastProposal; i++)
            {
                proposals.Add(await GetProposal(i).ConfigureAwait(false));
            }
            return proposals;
        }

        public async Task<Proposal> GetProposal(long index)
        {
            var proposalsFunction = contract.GetFunction("proposals");
            var proposal = await proposalsFunction.CallDeserializingToObjectAsync<Proposal>(index).ConfigureAwait(false);
            proposal.Index = index;
            return proposal;
        }

    }

}