using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaiexDashboard.Services.CountryCurrencyServices
{
    internal interface ICountryCurrencyService
    {
        CountryCurrency GetCountryCurrency(Guid UID);
        PagedResult<GetCountryCurrencyList_Result> GetAllCountryCurrency(JqueryDatatableParam param);
        string AddCountryCurrency(CountryCurrency countryCurrency);
        string UpdateCountryCurrency(CountryCurrency countryCurrency);
        List<Currency_Result> GetCurrencyByCountry(int id);
    }
}
