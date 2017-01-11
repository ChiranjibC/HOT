using BlockChainSI.Contracts;
using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockchainHOT.Controllers
{
    public class _LoggerController : Controller
    {
        private ILogger _logger;
        private IBatch _batch;
        private ITempLogger _device;
        private ITempRange _tempRange;
        public _LoggerController(ILogger logger, IBatch batch, ITempLogger device, ITempRange tempRange)
        {
            _logger = logger;
            _batch = batch;
            _device = device;
            _tempRange = tempRange;
        }
        //
        // GET: /Batch/
        public ActionResult Index()
        {
            return View(_logger.GetLogList(15, 2));
        }


        public JsonResult GetBatchItems()
        {
            return Json(_logger.GetLogList(15, 2), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Batch/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Batch/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Batch/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Batch/Edit/5
        public ActionResult Edit(string id)
        {
            Guid guid = Guid.Empty;
            Guid.TryParse(id, out guid);
            var logItem = guid != Guid.Empty ? _logger.GetDetails(guid) : new LoggerViewModel();

            var selectBatchList = _batch.GetBatchItems(20, 1)
                                        .Select(s => new {
                                            BatchId = s.BatchCode,
                                            BatchInfo = string.Format("{0}: {1}", s.BatchCode, s.Description)
                                        }).ToList();
            var selectDeviceList = _device.GetDeviceList(20, 1)
                                        .Select(s => new {
                                            DeviceId = s.TempLoggerId,
                                            DeviceInfo = string.Format("{0}: {1}", s.TempLoggerNo, s.TempLoggerShortName)
                                        }).ToList();
            var selectTempRangeList = _tempRange.GetTempRanges()
                                        .Select(s => new {
                                            TempRangeId = s.TempRangeId,
                                            TempRangeInfo = string.Format("{0}: {1}", s.TempRangeCode, s.TempRange)
                                        }).ToList();

            logItem.BatchList = new SelectList(selectBatchList, "BatchId", "BatchInfo");
            logItem.DeviceList = new SelectList(selectDeviceList, "DeviceId", "DeviceInfo");
            logItem.TempRangeList = new SelectList(selectTempRangeList, "TempRangeId", "TempRangeInfo");

            return PartialView(logItem);
        }

        [HttpPost]
        public ActionResult Edit(LoggerViewModel loggerViewModel)
        {
            try
            {
                var updatedLogItem = _logger.UpdateLogItem(loggerViewModel);
                return Json(new { Success = true, updatedLogItem.LoggerId });
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Batch/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Batch/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}