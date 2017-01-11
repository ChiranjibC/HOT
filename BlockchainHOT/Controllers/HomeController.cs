using BlockchainHOT.App_Start;
using Nethereum.Web.Sample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlockchainHOT.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
           // var newBatch = new Nethereum.Web.Sample.Model.Dao.CreateBatchInput();
           // //newBatch._batchId = new Nethereum.ABI.Bytes32Type("Batch14Dec2016Test01");
           // //newBatch._batchId = "Batch14Dec2016Test01";
           // newBatch._batchId = Encoding.UTF8.GetBytes("Batch05Jan2017Test-01" + DateTime.Now.Ticks.ToString().Substring(0,2));
           // newBatch._deviceId = SiteConstants.Devices.Device1; //0x0977EE50104A09e37B7E675b847d86C7d131fc91

           // newBatch._lstMinTemp = new short[4];
           // newBatch._lstMaxTemp = new short[4];
           // newBatch._lstExpireTickCount = new short[4];

           // newBatch._lstMinTemp[0] = 0;
           // newBatch._lstMaxTemp[0] = 15;
           // newBatch._lstExpireTickCount[0] = 9999;

           // newBatch._lstMinTemp[1] = 16;
           // newBatch._lstMaxTemp[1] = 20;
           // newBatch._lstExpireTickCount[1] = 40;

           // newBatch._lstMinTemp[2] = 21;
           // newBatch._lstMaxTemp[2] = 30;
           // newBatch._lstExpireTickCount[2] = 10;

           // newBatch._lstMinTemp[3] = 31;
           // newBatch._lstMaxTemp[3] = 9999;
           // newBatch._lstExpireTickCount[3] = 1;

           // //var service = GetDaoService();
           // //var proposals = Sync(service.GetLatestProposals(5));

           // var isValidOwner1 = Sync(GetOwnerService().IsValidOwner("0x5d13b7566fc79b4c55dd3c3d9b48df107232acd2"));
           // var isValidOwner = Sync(GetOwnerService().IsValidOwner("0x013CaDa165ED4C86D2607bC9BfD2Cb6869995A56"));

           //// var batch = Sync(GetBatchService().GetBatchInfo("B1121516"));

           // var result = Sync((new BatchService()).InitiateBatchTracking(newBatch));

            return View();
        }

        public T Sync<T>(Task<T> call)
        {
            return Task.Run(() => call).Result;
        }
               

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}