using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using server.Attributes;
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

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task Add(T item) 
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

                var entity = new Entities.Common.Entity 
                {
                    Id = item.Id,
                    Type = entityType
                };
                await _context.Set<Entities.Common.Entity>().AddAsync(entity);
            }

            await _context.Set<T>().AddAsync(item);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(w => w.Id == id);
        }

        public void Update(T item) 
        {
            // no content
        }
    }
}