using DataAccessLayer.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KuaiexDashboard.Repository
{
   public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Func<T, bool> condition = null);
        T GetById(object id);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> condition = null, params string[] columns);
        void Insert(T entity);
        void Update(T entity);
        void Delete(object id);
        PagedResult<T> GetPagedDataFromSP<T>(string storedProcedureName, int page = 1, int pageSize = 10) where T : class;
        PagedResult<T> GetPagedData(int page, int pageSize);
    }
}
