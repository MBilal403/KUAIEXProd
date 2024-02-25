using KuaiexDashboard.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Controllers
{
    [AuthorizeFilter]
    public class RoutingBankSettingsController : Controller
    {
        // GET: RoutingBankSettings
        public ActionResult Index()
        {
            return View();
        }
    }
}