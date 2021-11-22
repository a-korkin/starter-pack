using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using server.Entities.Base;

namespace server.Services 
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        Task<bool> Save();
        Task Add(T item);
        // T AddType(T item);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        void Update(T item);
    }
}