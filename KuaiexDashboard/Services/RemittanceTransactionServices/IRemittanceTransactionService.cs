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
        List<Remittance_TrnDetailDTO> GetRemitterTransactionList();
    }
}
