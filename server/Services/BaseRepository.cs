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

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public T Add(T item) 
        {
            var entity = new Entities.Common.Entity 
            {
                Id = item.Id,
                Type = typeof(T).FullName
            };
            _context.Set<Entities.Common.Entity>().Add(entity);
            
            _context.Set<T>().Add(item);

            return item;
        }

        public T AddType(T item)
        {
            _context.Set<T>().Add(item);
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

        public void Update(T item) 
        {
            // no content
        }
    }
}