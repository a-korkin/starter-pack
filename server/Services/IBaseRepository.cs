using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using server.Entities.Admin;
using server.Entities.Base;

namespace server.Services 
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<bool> SaveAsync();
        Task AddAsync(T item);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<User> GetByUserNameAsync(string userName);
        void Update(T item);

    }
}