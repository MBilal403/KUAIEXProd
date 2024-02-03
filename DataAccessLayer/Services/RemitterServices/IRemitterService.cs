using DataAccessLayer.Entities;
using KuaiexDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services.RemitterService
{
    public interface IRemitterService
    {
        /// <summary>
        /// Creates a new Remitter
        /// </summary>
        string CreateRemitter(Customer customer);




    }
}
