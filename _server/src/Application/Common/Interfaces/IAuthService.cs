using System;
using System.Threading.Tasks;
using Application.Common.Models.DTO.Admin;

namespace Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<Tuple<AuthOutDto, string>> LoginAsync(AuthInDto userAuth);
        Task<AuthOutDto> LogoutAsync(Guid userId);
    }
}