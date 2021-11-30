using server.DbContexts;
using server.Entities.Common;

namespace server.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository 
    {
        public PersonRepository(ApplicationContext context) : base(context) {}
    }
}