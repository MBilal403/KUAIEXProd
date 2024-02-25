using DataAccessLayer.Repository;
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
        T FindBy(Expression<Func<T, bool>> condition = null);
        List<T> GetAll(Expression<Func<T, bool>> condition = null, params Expression<Func<T, object>>[] columns);
        T GetbyId(int id, params Expression<Func<T, object>>[] columns);
        int Insert(T entity);
        void Update(T entity, string whereClause);
        void Delete(object id);
        PagedResult<T> GetPagedDataFromSP<T>(string storedProcedureName, int page = 1, int pageSize = 10) where T : class;
        PagedResult<T> GetPagedData(int page, int pageSize);
        List<TResult> GetDataFromSP<TResult>(string storedProcedureName) where TResult : class;
        List<TResult> GetAllWithJoins<TResult>(List<JoinInfo> joins, Func<TResult, bool> condition = null, string columns = null) where TResult : class;

    }
}
