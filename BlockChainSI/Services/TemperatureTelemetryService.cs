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

namespace BlockChainSI.Services
{
    public class TemperatureTelemetryService : BaseService
    {
        private readonly Web3 web3;        
        
        
        private Contract contract;
        public TemperatureTelemetryService()
        { }
        public TemperatureTelemetryService(Web3 web3, string address)
        {
            this.web3 = web3;
            this.contract = web3.Eth.GetContract(OWNER_ABI, address);
        }

        public string InputTemparatureTelemetry(TempTelemetryInput tempTelemetryInput)
        {
            var web3Srv = new Web3(BLOCK_CHAIN_URL);
            var unlockAccountResultSrv =
                web3Srv.Personal.UnlockAccount.SendRequestAsync(tempTelemetryInput.SenderAddress, BLOCK_CHAIN_PASSWD, new HexBigInteger(120));
            unlockAccountResultSrv.Wait();
            var contract = web3Srv.Eth.GetContract(TEMPTELEMETRY_ABI, TEMPTELEMETRY_CONTRACT_ADDRESS);
            var tempTelemetryFunction = contract.GetFunction("inputTemparatureTelemetry");
            var _EvtTempReceived = contract.GetEvent("TempReceived");

            //Error Event tracking
            Event _EvtError = contract.GetEvent("Error");
            var filterAllTask = _EvtError.CreateFilterAsync();
            filterAllTask.Wait();
            var filterAll = filterAllTask.Result;

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
            while (receipt == null && count++ <= 15)
            {
                Thread.Sleep(5000);
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

            return string.Empty;
        }
    }

}