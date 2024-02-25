using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KuaiexDashboard.Filters
{
    public class AuthorizeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Check if session variable exists (adjust as needed)
            if (filterContext.HttpContext.Session["UserName"] == null)
            {
                // Redirect to login page 
                filterContext.Result = new RedirectResult("/");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}