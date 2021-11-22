using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using server.Entities.Base;

namespace server.Services 
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<bool> SaveAsync();
        Task AddAsync(T item);
        // T AddType(T item);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        void Update(T item);
    }
}