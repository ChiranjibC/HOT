﻿using BlockchainHOT.Models;
using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockchainHOT.Controllers
{
    public class DeviceController : Controller
    {
        private IDevice _device;

        public DeviceController(IDevice device)
        {
            _device = device;
        }

        //
        // GET: /Device/
        public ActionResult Index()
        {
            return View(_device.GetDeviceList(15, 1));
        }

        public JsonResult GetDeviceItems()
        {
            return Json(_device.GetDeviceList(15, 1), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Device/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Device/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Device/Create
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
        // GET: /Device/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Device/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Device/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Device/Delete/5
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