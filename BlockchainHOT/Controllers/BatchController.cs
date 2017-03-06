using BlockchainHOT.Models;
using BlockChainSI.Contracts;
using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockchainHOT.Controllers
{
    public class BatchController : BaseController
    {
        private IBatchService _batch;
        public BatchController(IBatchService batch)
        {
            _batch = batch;
        }

        public ActionResult Index()
        {
            var batchList = _batch.GetBatchItems(15, 2);
            return View(batchList);
        }

        public JsonResult GetBatchItems()
        {
            return Json(_batch.GetBatchItems(15, 2), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult Edit(string id)
        {
            var batch = _batch.GetDetails(id);
            return PartialView(batch);
        }

        [HttpPost]
        public ActionResult Edit(BatchViewModel batchViewModel)
        {
            try
            {
                var updatedBatch = _batch.UpdateBatch(batchViewModel);
                return Json(new { success = true, message = updatedBatch.Description + " Updated!"});
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Method to refresh database data from Block chain
        /// </summary>
        /// <returns></returns>
        public ActionResult RefreshData()
        {
            return Json( new { success = _batch.RefreshDBFromBlockChain() }, JsonRequestBehavior.AllowGet);
        }        
    }
}
