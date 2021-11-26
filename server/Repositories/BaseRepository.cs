using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Attributes;
using server.DbContexts;
using server.Entities.Admin;
using server.Entities.Base;

namespace server.Repositories 
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private ApplicationContext _context { get; set; }

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task AddAsync(T item) 
        {
            DescriptionAttribute attribute =
                    (DescriptionAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(DescriptionAttribute));

            if (attribute != null) 
            {
                var entityType = await _context
                    .Set<server.Entities.Admin.EntityType>()
                    .Where(w => w.Schema == attribute.Schema)                    
                    .Where(w => w.TableName == attribute.TableName)
                    .FirstOrDefaultAsync();

                Guid id = Guid.NewGuid();
                item.Id = id;

                var entity = new Entities.Common.Entity 
                {
                    Id = id,
                    Type = entityType
                };
                await _context.Set<Entities.Common.Entity>().AddAsync(entity);
            }

            await _context.Set<T>().AddAsync(item);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public void Update(T item) 
        {
            // no content
        }

        public async Task<T> GetOneByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }
    }
}