using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    public class KUAIEX_PROD_Controller : Controller
    {
        // GET: KUAIEX_PROD_
        public ActionResult Index()
        {
            return View();
        }

        public int GetCountryIdByCountryName(string Country_Name)
        {
            int Country_Id = 0;

            try
            {

            }
            catch (Exception ex)
            {
                throw;
            }
            return 0;
        }
    }
}