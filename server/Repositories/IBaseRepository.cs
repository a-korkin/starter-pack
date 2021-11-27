using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using server.Entities.Admin;
using server.Entities.Base;

namespace server.Repositories 
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<bool> SaveAsync();
        Task AddAsync(T item);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetOneByAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(Guid id);
        void Update(T item);
        Task<bool> DeleteAsync(Guid itemId);
    }
}