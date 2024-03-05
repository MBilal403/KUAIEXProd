using DataAccessLayer.Helpers;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaiexDashboard.Services.RemitterTransactionService
{
    interface IRemitterTransactionService
    {
        PagedResult<Remittance_TrnDetailDTO> GetRemitterTransactionList(JqueryDatatableParam param);
    }
}
