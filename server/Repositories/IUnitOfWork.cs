using System.Threading.Tasks;
using server.Entities.Base;

namespace server.Repositories
{
    public interface IUnitOfWork
    {
        // IUserRepository Users{ get; }
        IRoleRepository Roles { get; }
        // IPersonRepository Persons { get; }
        // IEntityTypeRepository EntityTypes { get; }
        // IClaimRepository Claims { get; }

        IGenericRepository<T> Repository<T>() where T : BaseEntity;

        Task CompleteAsync();
    }
}