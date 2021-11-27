using System;
using System.Threading.Tasks;
using server.Entities.Admin;
using server.Models.DTO.Admin;

namespace server.Services
{
    public interface IAuthService
    {
        Task<Tuple<AuthOutDto, string>> LoginAsync(AuthInDto userAuth);
        Task<AuthOutDto> LogoutAsync(Guid userId);
    }
}