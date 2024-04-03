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
        List<Comparison_Lookup> GelAllComprison();
        PagedResult<Currency> GetAllCurrency(JqueryDatatableParam param);
        string AddCurrency(Currency currency);
        string UpdateCurrency(Currency currency);
        int SynchronizeRecords();
        string AddCurrencylimit(Remittance_Currency_Limit remittance_Currency_Limit);
        string DeleteCurrencylimit(Guid UID);
        List<RemittanceCurrencyLimitResult> GetAllCurrencylimitByCurrencyId(int Id);
        List<Currency> getAllCurrencies();
    }
}
