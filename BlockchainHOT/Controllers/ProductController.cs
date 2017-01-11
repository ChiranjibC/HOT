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
    public class ProductController : BaseController
    {
        private IProduct _product;

        public ProductController(IProduct product)
        {
            _product = product;
        }
        // GET: Product
        public ActionResult Index()
        {
            return View(_product.GetProducts(20,1));
        }

        /// <summary>
        /// Method to load the product detail
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult Edit(string Id)
        {
            Guid productGuid = Guid.Empty;
            Guid.TryParse(Id, out productGuid);
            var product = productGuid != Guid.Empty ? _product.GetDetails(productGuid) : new ProductViewModel();
            return PartialView(product);
        }

        /// <summary>
        /// Method to update the posted data
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(ProductViewModel product)
        {
            var updatedProduct = _product.UpdateProduct(product);
            return Json(new { Status = "success" });
        }
    }
}