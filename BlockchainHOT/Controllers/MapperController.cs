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
    public class MapperController : BaseController
    {
        private IMapper _mapper;
        public MapperController(IMapper mapper)
        {
            _mapper = mapper;
        }
        //
        // GET: /Batch/
        public ActionResult Index()
        {
            return View(_mapper.GetMapList(15, 2));
        }


        public JsonResult GetBatchItems()
        {
            return Json(_mapper.GetMapList(15, 2), JsonRequestBehavior.AllowGet);
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
            var device = guid != Guid.Empty ? _mapper.GetDetails(guid) : new MapperViewModel();
            return PartialView(device);
        }

        //
        // POST: /Batch/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MapperViewModel mapperViewModel)
        {
            try
            {
                var updatedMapItem = _mapper.UpdateMapItem(mapperViewModel);
                return Json(new { Success = true, updatedMapItem.MapId });
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

        #region Private Helper methods

        

        

        

        #endregion
    }
}
