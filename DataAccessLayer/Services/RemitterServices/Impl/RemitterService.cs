using DataAccessLayer.Entities;
using DataAccessLayer.Recources;
using DataAccessLayer.Services.RemitterService;
using KuaiexDashboard;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Services.RemitterServices.Impl
{
    public class RemitterService : IRemitterService
    {
        IRepository<Customer> _customerRepository;
        public RemitterService()
        {
            _customerRepository = new GenericRepository<Customer>();
        }
        public string CreateRemitter(Customer customer)
        {
            _customerRepository.Insert(customer);

            return MsgKeys.CreatedSuccessfully;

        }


    }
}
