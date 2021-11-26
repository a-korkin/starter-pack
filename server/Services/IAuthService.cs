using System;
using System.Threading.Tasks;
using server.Entities.Admin;
using server.Models.DTO.Admin;

namespace server.Services
{
    public interface IAuthService
    {
        Task<User> GetByUserNameAsync(string userName);
        Task<Tuple<AuthOutDto, string>> LoginAsync(AuthInDto userAuth);
    }
}