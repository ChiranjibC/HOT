using BlockchainHOT.App_Start;
using Nethereum.Web.Sample.Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var _newBatch = GetBatchInputParam();
            var batchService = new BatchService();
            var status = Sync(batchService.testInitiateBatch(_newBatch));

            //var updatedBatch = Sync(batchService.InitiateBatchTracking(_newBatch));
        }

        public static T Sync<T>(Task<T> call)
        {
            return Task.Run(() => call).Result;
        }
        private static CreateBatchInput GetBatchInputParam()
        {
            var newBatch = new CreateBatchInput();
            newBatch._batchId = Encoding.UTF8.GetBytes("Batch05Jan2017Test-01" + DateTime.Now.Ticks.ToString().Substring(0, 2));
            newBatch._deviceId = SiteConstants.Devices.Device1; //0x0977EE50104A09e37B7E675b847d86C7d131fc91

            newBatch._lstMinTemp = new short[4];
            newBatch._lstMaxTemp = new short[4];
            newBatch._lstExpireTickCount = new short[4];

            newBatch._lstMinTemp[0] = 0;
            newBatch._lstMaxTemp[0] = 15;
            newBatch._lstExpireTickCount[0] = 9999;

            newBatch._lstMinTemp[1] = 16;
            newBatch._lstMaxTemp[1] = 20;
            newBatch._lstExpireTickCount[1] = 40;

            newBatch._lstMinTemp[2] = 21;
            newBatch._lstMaxTemp[2] = 30;
            newBatch._lstExpireTickCount[2] = 10;

            newBatch._lstMinTemp[3] = 31;
            newBatch._lstMaxTemp[3] = 9999;
            newBatch._lstExpireTickCount[3] = 1;
            return newBatch;
        }
    }
}
