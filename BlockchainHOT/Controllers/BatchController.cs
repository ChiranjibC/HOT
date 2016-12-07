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
        private IBatch _batch;
        private IProduct _product;
        public BatchController(IBatch batch, IProduct product)
        {
            _batch = batch;
            _product = product;
        }
        //
        // GET: /Batch/
        public ActionResult Index()
        {
            var batchList = _batch.GetBatchItems(15, 2);
            PopulateProduct(batchList);
            return View(batchList);
        }

        public JsonResult GetBatchItems()
        {
            return Json(_batch.GetBatchItems(15, 2), JsonRequestBehavior.AllowGet);
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
            var batch = guid != Guid.Empty ? _batch.GetDetails(guid) : new BatchViewModel();
            batch.Products = GetProductSelectList();            
            return PartialView(batch);
        }

        private SelectList GetProductSelectList()
        {
            return new SelectList(_product.GetProducts(10000, 0), "ProductId", "ProductDesc");
        }

        private IEnumerable<ProductViewModel> GetProducts()
        {
            return _product.GetProducts(10000, 0);
        }
        
        private void PopulateProduct(IEnumerable<BatchViewModel> batchList)
        {
            var products = GetProducts();
            foreach (BatchViewModel batch in batchList)
            {
                var matchedProduct = products.Where(x => x.ProductId == batch.ProductId).FirstOrDefault();
                batch.Product = matchedProduct != null ? matchedProduct.ProductDesc : "Temp Entry";
            }
        }

        //
        // POST: /Batch/Edit/5
        [HttpPost]
        public ActionResult Edit(BatchViewModel batchViewModel)
        {
            try
            {
                var updatedBatch = _batch.UpdateBatch(batchViewModel);
                return Json(new { Success = true, updatedBatch.BatchId });
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
