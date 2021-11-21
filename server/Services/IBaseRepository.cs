using System;
using System.Collections.Generic;
using server.Entities.Base;

namespace server.Services 
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        T Create(T item);
        IEnumerable<T> GetAll();
        T GetById(Guid id);
    }
}