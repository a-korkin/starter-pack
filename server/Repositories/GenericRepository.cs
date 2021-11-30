using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using server.Attributes;
using server.DbContexts;
using server.Entities.Admin;
using server.Entities.Base;

namespace server.Repositories 
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(
            ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task AddAsync(T item) 
        {
            DescriptionAttribute attribute =
                    (DescriptionAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(DescriptionAttribute));

            Guid id = Guid.NewGuid();
            item.Id = id;
            
            if (attribute != null) 
            {
                var entityType = await _context
                    .Set<server.Entities.Admin.EntityType>()
                    .Where(w => w.Schema == attribute.Schema)                    
                    .Where(w => w.TableName == attribute.TableName)
                    .FirstOrDefaultAsync();

                var entity = new Entities.Common.Entity 
                {
                    Id = id,
                    Type = entityType
                };
                await _context.Set<Entities.Common.Entity>().AddAsync(entity);
            }

            await _dbSet.AddAsync(item);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual async Task<T> GetOneByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(w => w.Id == id);
        }

        public virtual async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _dbSet.AnyAsync(w => w.Id == id);
        }

        public virtual async Task<bool> ExistsByExpAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }

        public virtual async Task<bool> DeleteAsync(Guid itemId)
        {
            var item = await GetByIdAsync(itemId);

            if (item != null)
            {
                _dbSet.Remove(item);
                return true;
            }
            return false;
        }
    }
}