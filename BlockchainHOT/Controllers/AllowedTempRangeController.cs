using BlockChainSI.Models;
using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockchainHOT.Controllers
{
    public class AllowedTempRangeController : BaseController
    {
        public IAllowedBatchTempRangesService _allowedTempService;
        public AllowedTempRangeController(IAllowedBatchTempRangesService allowedTempService)
        {
            _allowedTempService = allowedTempService;
        }        
        public ActionResult Index(string Id)
        {
            var allowedTempRanges = _allowedTempService.GetBatchAllowedTempDetails(Id);
            return PartialView(allowedTempRanges);
        }        

        [HttpPost]
        public ActionResult Index(StabilityRangeListViewModel allowedTempList)
        {
            var status = _allowedTempService.UpdateAllowedBatchTemp(allowedTempList);
            if (status)
            {
                return RedirectToAction("index", "Batch");
            }
            return Json(new { Status = "Error" });
        }
    }
}