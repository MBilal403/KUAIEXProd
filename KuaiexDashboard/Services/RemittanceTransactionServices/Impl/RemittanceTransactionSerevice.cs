using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.DTO;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Services.RemitterTransactionService.Impl
{
    public class RemitterTransactionSerevice : IRemitterTransactionService
    {
        IRepository<Remittance_Trn> _remittance_TrnRepository;
        public RemitterTransactionSerevice()
        {
            _remittance_TrnRepository = new GenericRepository<Remittance_Trn>();
        }

        public PagedResult<Remittance_TrnDetailDTO> GetRemitterTransactionList(JqueryDatatableParam param)
        {
            try
            {
                PagedResult<Remittance_TrnDetailDTO> getCurrencyList_Results = _remittance_TrnRepository.GetPagedDataFromSP<Remittance_TrnDetailDTO>("GetRemitterTransactionListWithPagination", param.iDisplayStart + 1, param.iDisplayLength, param.sSearch);
                return getCurrencyList_Results;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }

        }
    }
}