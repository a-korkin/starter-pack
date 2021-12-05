using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interface;
using Application.Common.Models.DTO.Admin;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(
            ApplicationContext context, 
            IConfiguration configuration)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Tuple<AuthOutDto, string>> LoginAsync(AuthInDto userAuth)
        {
            var userEntity = await _context.Users
                .Where(w => w.UserName == userAuth.UserName)
                .FirstOrDefaultAsync();
            
            throw new NotImplementedException();
        }

        public Task<AuthOutDto> LogoutAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}