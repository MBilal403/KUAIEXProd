using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaiexDashboard.Services.BranchServices
{
    internal interface IBranchService
    {
        int SynchronizeRecords();
        PagedResult<GetBankBranchesList_Result> GetAllBankBranches(JqueryDatatableParam param, int countryId, int bankId);

    }
}
