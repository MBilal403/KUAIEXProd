using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Services.BankServices
{
    interface IBankService
    {
        PagedResult<GetBanks_Result> GetAllWithPagination(JqueryDatatableParam param, int countryId);
        GetBankDetailsById_Result GetByUID(Guid uid);
        Bank_Mst GetLimitDetailByUId(Guid uid);
        string UpdateLimits(Guid? UID, decimal AmountLimit, int NumberOfTransaction);
        int SynchronizeRecords();
        string ChangeState(Guid UID);

    }
}