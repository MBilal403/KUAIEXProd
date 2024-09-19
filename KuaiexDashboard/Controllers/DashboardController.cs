using DataAccessLayer;
using DataAccessLayer.Recources;
using KuaiexDashboard.Filters;
using KuaiexDashboard.Services.DashboardServices;
using KuaiexDashboard.Services.DashboardServices.Impl;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.DomainEntities;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController()
        {
            _dashboardService = new DashboardService();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadGrid()
        {
            string status = "error";
            try
            {
                List<GetCurrencyRate_Result> result = _dashboardService.GetCurrencyRates();
                status = Newtonsoft.Json.JsonConvert.SerializeObject(result.Take(10));
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult TotalCustomers()
        {
            string status = "error";
            try
            {
                var result = _dashboardService.GetTotalCustomerCount();
                status = result.ToString();
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }

            return Content(status);
        }
        public ActionResult TodayCustomers()
        {
            string status = "error";
            try
            {
                var result = _dashboardService.GetTodayCustomerCount();
                status = result.ToString();
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult TotalReviwed()
        {
            string status = "error";
            try
            {
                var result = _dashboardService.GetTotalCustomerReviewedCount();
                status = result.ToString();
            }
            catch (Exception ex)
            {
                Log.Error(@"{Message}: {e}", ex.Message, ex);
                status = "error";
            }
            return Content(status);
        }
        public ActionResult TodayReviwed()
        {
            string status = "error";
            try
            {
                var result = _dashboardService.GetTodayCustomerReviewedCount();
                status = result.ToString();
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