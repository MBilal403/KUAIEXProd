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
        string UpdateLimits(Guid? UID, decimal AmountLimit, int NumberOfTransaction, int NumberOfTransactionMonthly, decimal TxnAmountKWD, decimal TxnAmountFC);
        List<Bank_Mst> GetBanksByCountryId(int countryId);
        int SynchronizeRecords();
        string ChangeState(Guid UID);
        string ChangeDirectTransaction(int id);
        string SetBankPriority(Bank_Mst[] bank_MstList);

    }
}