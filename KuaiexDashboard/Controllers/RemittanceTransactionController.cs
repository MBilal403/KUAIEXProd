using DataAccessLayer.Entities;
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
        public ActionResult LoadGrid()
        {
            string status = "error";

            try
            {
                List<Remittance_TrnDetailDTO> result = _remitterTransactionService.GetRemitterTransactionList();

                status = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }
    }
}