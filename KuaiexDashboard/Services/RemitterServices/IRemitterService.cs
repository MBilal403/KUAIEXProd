using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.ProcedureResults;
using DataAccessLayer.Repository.Impl;
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
        string EditRemitter(CustomerDTO customerDto);
        EditCustomerDTO GetCustomerByUID(Guid customerId);
        PagedResult<GetRemitterList_Result> GetRemitterList(JqueryDatatableParam param);
        List<Customer_Security_Questions> GetCustomerSecurityQuestions(int id);
        List<Individual_KYC> GetCustomerLoadKYCIndividuals(int id);
    }
}
