using KuaiexDashboard.Profile;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace KuaiexDashboard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Map the virtual path to a physical path using Server.MapPath
            string logDirectory = Server.MapPath("~/Logs");
            if (!System.IO.File.Exists(logDirectory))
            {
            // Ensure the Log Directory Exists
                System.IO.Directory.CreateDirectory(logDirectory);
            }
            // Configure Serilog

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File($"{logDirectory}\\log-.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                .CreateLogger();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            MapperProfile.Run();
        }
    }
}
