using System.Threading.Tasks;

namespace server.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users{ get; }

        Task CompleteAsync();
    }
}