using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
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
            //if (IsAdminUser)
            //{
            List<GetCurrencyRate_Result> result = objDashDal.GetCurrencyRates();
            status = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            //}
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
                status = "error";
            }

            return Content(status);
        }
        public ActionResult TodayCustomers()
        {
            string status = "error";
            //if (IsAdminUser)
            //{
            var result = objDashDal.GetTodayCustomerCount();
            status = result.ToString();
            //}
            return Content(status);
        }
        public ActionResult TotalReviwed()
        {
            string status = "error";
            //if (IsAdminUser)
            //{
            var result = objDashDal.GetTotalCustomerReviewedCount();
            status = result.ToString();
            //}
            return Content(status);
        }
        public ActionResult TodayReviwed()
        {
            string status = "error";
            //if (IsAdminUser)
            //{
            var result = objDashDal.GetTodayCustomerReviewedCount();
            status = result.ToString();
            //}
            return Content(status);
        }
    }
}