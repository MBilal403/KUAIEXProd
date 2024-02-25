using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Services.CityServices
{
    public interface ICityService
    {
        List<City> GetActiveCities();

    }
}