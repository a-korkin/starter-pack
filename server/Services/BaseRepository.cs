using System;
using System.Collections.Generic;
using System.Linq;
using server.DbContexts;
using server.Entities.Base;

namespace server.Services 
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private ApplicationContext _context { get; set; }

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public T Create(T item) 
        {
            _context.Set<T>().Add(item);
            _context.SaveChanges();
            
            return item;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(Guid id)
        {
            return _context.Set<T>().FirstOrDefault(w => w.Id == id);
        }
    }
}