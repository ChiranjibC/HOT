using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using BlockChainSI.Dao;
using System.Threading;
using System.Text;
using Nethereum.ABI.Encoders;

namespace BlockChainSI.SIServices
{
    /// <summary>
    /// Temperature Telemetry System Interface service to connect to Block Chain
    /// </summary>
    public class TemperatureTelemetrySIService : BaseSIService
    {
        private readonly Web3 web3;        
        
        
        private Contract contract;
        public TemperatureTelemetrySIService()
        { }
        public TemperatureTelemetrySIService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(OWNER_ABI, address);
        }

        /// <summary>
        /// This method is used to create a temperature log entry with current temperature of the Temp Logger device
        /// associated with a Batch.
        /// This would trigger Smart Contract to expire the Batch, if the number of Stability entries exceeed the allowable Batch 
        /// entries for the given temperature range (setup when creating a new Batch).
        /// </summary>
        /// <param name="tempTelemetryInput"></param>
        /// <returns></returns>
        public string InputTemparatureTelemetry(TempTelemetryInput tempTelemetryInput)
        {
            var web3Srv = new Web3(BLOCK_CHAIN_URL);
            var unlockAccountResultSrv =
                web3Srv.Personal.UnlockAccount.SendRequestAsync(tempTelemetryInput.SenderAddress, BLOCK_CHAIN_PASSWD, new HexBigInteger(120));
            unlockAccountResultSrv.Wait();
            var contract = web3Srv.Eth.GetContract(TEMPTELEMETRY_ABI, TEMPTELEMETRY_CONTRACT_ADDRESS);
            var tempTelemetryFunction = contract.GetFunction("inputTemparatureTelemetry");

            //Error Event tracking
            Event _EvtError = contract.GetEvent("Error");
            var filterAllTask = _EvtError.CreateFilterAsync();
            filterAllTask.Wait();
            var filterAll = filterAllTask.Result;

            Event _EvtTempReceived = contract.GetEvent("TempReceived");
            var filterTempReceivedTask = _EvtTempReceived.CreateFilterAsync();
            filterTempReceivedTask.Wait();
            var filterTempReceived = filterTempReceivedTask.Result;

            var resultBatchTask = tempTelemetryFunction.SendTransactionAsync(tempTelemetryInput.SenderAddress,
                                                                        new HexBigInteger(500000),
                                                                        new HexBigInteger(0),
                                                                        tempTelemetryInput._batchId,
                                                                        tempTelemetryInput._temp
                                                                        );
            resultBatchTask.Wait();
            var resultBatch = resultBatchTask.Result;
            var receiptTask = web3Srv.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(resultBatch);
            receiptTask.Wait();
            var receipt = receiptTask.Result;
            int count = 0;
            while (receipt == null && count++ <= 25)
            {
                Thread.Sleep(1500);
                receiptTask = web3Srv.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(resultBatch);
                receiptTask.Wait();
                receipt = receiptTask.Result;
            }

            //Error Event tracking
            var logTask = _EvtError.GetFilterChanges<ErrorEvent>(filterAll);
            logTask.Wait();
            var log = logTask.Result;
            if (log != null && log.Count > 0)
            {
                string logErrorInfo = Encoding.UTF8.GetString(log[0].Event.Info);
                return logErrorInfo;
            }

            //Event TempReceived 
            var tempReceivedTask = _EvtTempReceived.GetFilterChanges<TempReceivedEvent>(filterTempReceived);
            tempReceivedTask.Wait();
            var tempReceived = tempReceivedTask.Result;
            if (tempReceived != null && tempReceived.Count > 0)
            {
                var tempInfo = tempReceived[0].Event.Temp;
                return (tempTelemetryInput._temp == tempInfo ? 
                                                    string.Empty 
                                                    : "Temperature Input not captured") ;
            }
            
            //var tempHistory = GetTemperaturHistorySync(tempTelemetryInput._batchId);
            //if (tempHistory != null && tempHistory.ListTemperature != null)
            //{
            //    for (int i = 0; i < tempHistory.ListTime.Count(); i++)
            //    {
            //        var entryTime = tempHistory.ListTime[i].ToString();
            //        var entryTemp = tempHistory.ListTemperature[i].ToString();
            //    }
            //}
            return string.Empty;
        }

        /// <summary>
        /// No UI for this method.
        /// This method is used the list of temperature entries made for the given batches.
        /// Helpful to debug in case batch is expected to be expired due to high temp entries, but unable to validate it.
        /// </summary>
        /// <param name="batchCode"></param>
        /// <returns></returns>
        public TemperaturHistoryDao GetTemperaturHistorySync(byte[] batchCode)
        {
            try
            {
                var web3 = new Web3(BLOCK_CHAIN_URL);
                contract = web3.Eth.GetContract(TEMPTELEMETRY_ABI, TEMPTELEMETRY_CONTRACT_ADDRESS);                                
                var getTemperaturHistory = contract.GetFunction("getTemperaturHistory");

                var temperaturHistoryDetailsTask = getTemperaturHistory.CallDeserializingToObjectAsync<TemperaturHistoryDao>(batchCode, 0);
                temperaturHistoryDetailsTask.Wait();
                return temperaturHistoryDetailsTask.Result;
            }
            catch (Exception ex)
            {
                string exmsg = ex.ToString();
                return null;
            }
        }
    }

}