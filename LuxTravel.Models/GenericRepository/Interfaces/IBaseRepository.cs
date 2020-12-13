using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LuxTravel.Models.GenericRepository.Interfaces
{
    public interface IBaseRepository<T , TContext> 
        where T : class
        where TContext : class

    {
        Task<IEnumerable<T>> GetAll();
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] includes);
        Task<T> GetById(int id);
        Task<T> GetById(Guid id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
        IQueryable<T> GetMany(Expression<Func<T, bool>> expression);

        IQueryable<T> GetMany(Expression<Func<T, bool>> predicate = null,
            params Expression<Func<T, object>>[] includes);
    }
}
