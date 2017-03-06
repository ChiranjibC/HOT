using BlockChainSI.Contracts;
using BlockChainSI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockchainHOT.Controllers
{
    public class OwnershipController : Controller
    {
        private IOwnershipService _ownership;
        public OwnershipController(IOwnershipService ownership)
        {
            _ownership = ownership;
        }
        public ActionResult Index()
        {
            var ownerShipList = _ownership.GetOwnerShipDetails(10, 10);
            return View(ownerShipList);
        }

        public ActionResult Edit(string Id)
        {
            var ownerShipItem = _ownership.GetDetails(Id);
            return PartialView(ownerShipItem);
        }

        [HttpPost]
        public ActionResult Edit(BatchOwnershipHistoryViewModel ownershipViewModel)
        {
            try
            {
                var updatedOwnershipStatus = _ownership.Update(ownershipViewModel);
                return Json(new { success = string.IsNullOrEmpty(updatedOwnershipStatus), message = updatedOwnershipStatus });
            }
            catch
            {
                return PartialView(ownershipViewModel);
            }
        }
    }
}