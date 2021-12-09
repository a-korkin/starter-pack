using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Attributes;
using Domain.Entities.Base;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(
            IApplicationDbContext context,
            DbSet<TEntity> dbSet)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

            _dbSet = dbSet ??
                throw new ArgumentNullException(nameof(dbSet));
        }

        public async Task AddAsync(TEntity item)
        {
            DescriptionAttribute descriptionAttribute =
                (DescriptionAttribute)Attribute.GetCustomAttribute(typeof(TEntity), typeof(DescriptionAttribute));

            if (descriptionAttribute != null)
            {
                var entityType = await _context.EntityTypes
                    .Where(w => w.Schema == descriptionAttribute.Schema)
                    .Where(w => w.TableName == descriptionAttribute.TableName)
                    .FirstOrDefaultAsync();
                
                var entity = new Entity
                {
                    Id = item.Id,
                    Type = entityType
                };
                await _context.Entities.AddAsync(entity);
            }

            await _dbSet.AddAsync(item);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}