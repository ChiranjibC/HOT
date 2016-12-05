using BlockChainSI.Contracts;
using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockchainHOT.Controllers
{
    public class LoggerController : Controller
    {
        private ILogger _logger;
        public LoggerController(ILogger logger)
        {
            _logger = logger;
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Batch/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, LoggerViewModel loggerViewModel)
        {
            try
            {
                // TODO: Add update logic here
                if (string.IsNullOrEmpty(loggerViewModel.BatchNumber))
                {
                    return RedirectToAction("Index");
                }
                var updatedBatch = _logger.UpdateLogItem(loggerViewModel);
                return Json(new { Success = true, updatedBatch.LoggerId });
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