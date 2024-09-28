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
        int GetCustomerIdByUID(Guid customerUID);
        PagedResult<GetRemitterList_Result> GetRemitterList(DataTableParams param);
        List<Customer_Security_Questions> GetCustomerSecurityQuestions(int id);
        List<Individual_KYC> GetCustomerLoadKYCIndividuals(int id);
        List<Residency_Type> GetResidencyTypes();
        List<IdentificationTypeLookup> GetAllIdentificationTypes();
        List<Transaction_Count_Lookup> GetAllTransactionCountLookup();
        List<Transaction_Amount_Lookup> GetAllTransactionAmountLookup();
        List<Gender_Lookup> GetAllGenderLookup();
    }
}
