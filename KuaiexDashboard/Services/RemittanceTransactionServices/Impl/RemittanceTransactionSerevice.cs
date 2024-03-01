using DataAccessLayer.Entities;
using DataAccessLayer.Recources;
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

        public List<Remittance_TrnDetailDTO> GetRemitterTransactionList()
        {
            try
            {
                return _remittance_TrnRepository.GetDataFromSP<Remittance_TrnDetailDTO>("GetRemitterTransactionList");
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }
    }
}