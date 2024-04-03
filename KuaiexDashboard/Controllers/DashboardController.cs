using DataAccessLayer;
using DataAccessLayer.Recources;
using KuaiexDashboard.Filters;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class DashboardController : Controller
    {
        DashboardDAL objDashDal = new DashboardDAL();

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoadGrid()
        {
            string status = "error";
            try
            {
                List<GetCurrencyRate_Result> result = objDashDal.GetCurrencyRates();
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
                var result = objDashDal.GetTotalCustomerCount();
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
                var result = objDashDal.GetTodayCustomerCount();
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
                var result = objDashDal.GetTotalCustomerReviewedCount();
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
                var result = objDashDal.GetTodayCustomerReviewedCount();
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