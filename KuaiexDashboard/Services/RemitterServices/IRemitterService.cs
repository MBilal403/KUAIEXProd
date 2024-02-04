using KuaiexDashboard.DTO.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaiexDashboard.Services.RemitterServices
{
    interface IRemitterService
    {
        string CreateRemitter(CustomerDTO customerDto);
    }
}
