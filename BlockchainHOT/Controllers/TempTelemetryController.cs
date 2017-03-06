using BlockchainHOT.Common;
using BlockChainSI.Contracts;
using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockchainHOT.Controllers
{
    public class TempTelemetryController : Controller
    {
        private ITempTelemetryService _tempTelemetry;
        public TempTelemetryController(ITempTelemetryService tempTelemetry)
        {
            _tempTelemetry = tempTelemetry;
        }
        public ActionResult Index()
        {
            var ownerShipList = _tempTelemetry.GetTelemetryHistory(10, 10);
            return View(ownerShipList);
        }

        public ActionResult Edit(string Id)
        {
            var tempTelemetry = _tempTelemetry.Get();
            return PartialView(tempTelemetry);
        }

        [HttpPost]
        public ActionResult Edit(TemparatureTelemetryViewModel tempTelemetryViewModel)
        {
            try
            {
                var updatedStatus = _tempTelemetry.Update(tempTelemetryViewModel);
                return Json(new { success = string.IsNullOrEmpty(updatedStatus), message = updatedStatus });
            }
            catch
            {
                return PartialView(tempTelemetryViewModel);
            }
        }

        [HttpPost]
        public ActionResult BulkUpload(HttpPostedFileBase fileDoc)
        {
            try
            {
                var batchCode = HttpContext.Request.Form["SelectBatch.BatchCode"];
                var tempTelemetryBulkData = BulkUtility.GetDataSet(fileDoc);
                var updatedStatus = _tempTelemetry.BulkUpdate(tempTelemetryBulkData, batchCode);
                if (!string.IsNullOrEmpty(updatedStatus))
                {
                    var tempTelemetryList = _tempTelemetry.GetTelemetryHistory(10, 10);
                    tempTelemetryList.ErrorMsg = "Error processing Bulk upload! /n" + updatedStatus;
                    return View("Index", tempTelemetryList);
                }
                else
                {
                    return RedirectToAction("Index");
                }                
            }
            catch (Exception ex)
            {
                var tempTelemetryList = _tempTelemetry.GetTelemetryHistory(10, 10);
                tempTelemetryList.ErrorMsg = "Error processing Bulk upload! <br/>" + ex.ToString();
                return View("Index", tempTelemetryList);
            }
        }
    }
}