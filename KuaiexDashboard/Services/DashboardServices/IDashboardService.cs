using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DomainEntities;


namespace KuaiexDashboard.Services.DashboardServices
{
    internal interface IDashboardService
    {
        List<GetCurrencyRate_Result> GetCurrencyRates();
        int GetTotalCustomerCount();
        int GetTodayCustomerCount();
        int GetTotalCustomerReviewedCount();
        int GetTodayCustomerReviewedCount();
    }
}
