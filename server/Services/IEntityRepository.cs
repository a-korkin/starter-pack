using System;
using System.Collections.Generic;
using server.Entities.Common;

namespace server.Services 
{
    public interface IEntityRepository<T> where T : Entity 
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
    }
}