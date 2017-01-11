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
        private ITempTelemetry _tempTelemetry;
        public TempTelemetryController(ITempTelemetry tempTelemetry)
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
    }
}