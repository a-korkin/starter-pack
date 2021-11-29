using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using server.DbContexts;

namespace server.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext _context;
        public IUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            
            Users = new UserRepository(context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}