using BlockChainSI.Models;
using BlockChainSI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockchainHOT.Controllers
{
    public class StabilityController : BaseController
    {
        public IStability _stability;
        public ITempRange _tempRange;
        public StabilityController(IStability stability, ITempRange tempRange)
        {
            _stability = stability;
            _tempRange = tempRange;
        }
        // GET: Stability
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetStabilityDetails(Guid productId)
        {
            return Json(_stability.GetProductStabilityDetails(productId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult StabilityInfo(string Id)
        {
            Guid productGuid = Guid.Empty;
            if (string.IsNullOrEmpty(Id) || !Guid.TryParse(Id, out productGuid))
            {
                productGuid = Guid.NewGuid();
            }

            var selectListTempRanges =  _tempRange.GetTempRanges()
                                        .Select(s => new {
                                                       TempRangeId = s.TempRangeId.ToString(),
                                                       TempRange = string.Format("{0}: {1}", s.TempRangeCode, s.TempRange)
                                                   }).ToList();

            ViewBag.TempRangesList = new SelectList(selectListTempRanges, "TempRangeId", "TempRange");
            return PartialView(_stability.GetProductStabilityDetails(productGuid));
        }

        public ActionResult StabilityList(string Id)
        {
            Guid productGuid = Guid.Empty;
            if (string.IsNullOrEmpty(Id) || !Guid.TryParse(Id, out productGuid))
            {
                productGuid = Guid.NewGuid();
            }
            var selectListTempRanges = _tempRange.GetTempRanges()
                                        .Select(s => new {
                                            TempRangeId = s.TempRangeId.ToString(),
                                            TempRange = string.Format("{0}: {1}", s.TempRangeCode, s.TempRange)
                                        }).ToList();

            StabilityListViewModel stabilityList = new StabilityListViewModel();
            stabilityList.StabilityDetails = _stability.GetProductStabilityDetails(productGuid);
            stabilityList.TempRangeList = new SelectList(selectListTempRanges, "TempRangeId", "TempRange");
            return PartialView(stabilityList);
        }

        public JsonResult Delete(string Id)
        {
            Guid stabilityGuid = Guid.Empty;
            if (string.IsNullOrEmpty(Id) || !Guid.TryParse(Id, out stabilityGuid))
            {
                stabilityGuid = Guid.NewGuid();
            }
            var status = _stability.DeleteStabilityItem(stabilityGuid);
            return Json(new { Status = status ? "Success" : "Error" });
        }

        public ActionResult Save(IList<StabilityChartViewModel> productStability)
        {
            Guid stabilityGuid = Guid.Empty;
            var status = _stability.UpdateAllProductStability(productStability);
            if (status)
            {
                return RedirectToAction("index", "product");
            }
            return Json(new { Status = "Error" });
        }
    }
}