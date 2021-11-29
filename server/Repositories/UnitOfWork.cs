using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using server.DbContexts;

namespace server.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext _context;
        // private readonly ILogger _logger;
        public IUserRepository Users { get; private set; }

        public UnitOfWork(
            ApplicationContext context)
            // ILoggerFactory loggerFactory)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
            
            // _logger = loggerFactory.CreateLogger("db_logs") ??
            //     throw new ArgumentNullException(nameof(loggerFactory));

            Users = new UserRepository(context); //, _logger);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}