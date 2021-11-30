using System.Threading.Tasks;

namespace server.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users{ get; }
        IRoleRepository Roles { get; }
        IPersonRepository Persons { get; }
        IEntityTypeRepository EntityTypes { get; }
        IClaimRepository Claims { get; }

        Task CompleteAsync();
    }
}