using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.DTO;
using KuaiexDashboard.Filters;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Services.RemitterTransactionService;
using KuaiexDashboard.Services.RemitterTransactionService.Impl;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class RemittanceTransactionController : Controller
    {
        IRemitterTransactionService _remitterTransactionService;
        public RemittanceTransactionController()
        {
            _remitterTransactionService = new RemitterTransactionSerevice();
        }
        // GET: RemitterTransaction
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LoadGrid(JqueryDatatableParam param)
        {
            try
            {
                PagedResult<Remittance_TrnDetailDTO> list = _remitterTransactionService.GetRemitterTransactionList(param);

                var result = new
                {
                    draw = param.sEcho,
                    recordsTotal = list.TotalSize,
                    recordsFiltered = list.FilterRecored,
                    data = list.Data
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                return Json("Error", JsonRequestBehavior.AllowGet);
            }


        }
    }
}