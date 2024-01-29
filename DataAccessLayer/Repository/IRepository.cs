using DataAccessLayer.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuaiexDashboard.Repository
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Func<T, bool> condition = null);
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
        PagedResult<T> GetPagedData(int page, int pageSize);
    }
}
