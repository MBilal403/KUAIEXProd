using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaiexDashboard.Services.CurrencyServices
{
    internal interface ICurrencyService
    {
        Currency GetCurrency(Guid UID);
        PagedResult<Currency> GetAllCurrency(JqueryDatatableParam param);
        string AddCurrency(Currency currency);
        string UpdateCurrency(Currency currency);
    }
}
