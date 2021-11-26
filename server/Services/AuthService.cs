using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using server.DbContexts;
using server.Entities.Admin;
using server.Models.DTO.Admin;
using SystemClaims = System.Security.Claims;

namespace server.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(
            ApplicationContext context, 
            IConfiguration configuration, 
            IMapper mapper)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));

            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
            
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<User> GetByUserNameAsync(string userName)
        {
            return await _context.Set<User>()
                .SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<Tuple<AuthOutDto, string>> LoginAsync(AuthInDto userAuth)
        {
            var userEntity = await GetByUserNameAsync(userAuth.UserName);
                
            if (userEntity != null && 
                BCrypt.Net.BCrypt.Verify(userAuth.Password, userEntity.Password))
            {
                var claims = new SystemClaims.Claim[] 
                {
                    new SystemClaims.Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new SystemClaims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new SystemClaims.Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new SystemClaims.Claim("id", userEntity.Id.ToString()),
                    new SystemClaims.Claim("userName", userEntity.UserName)
                }; 

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var accessToken = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: signIn
                );

                var refreshToken = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: signIn
                );

                userEntity.RefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);
                await _context.SaveChangesAsync();


                var authToReturn = _mapper.Map<AuthOutDto>(userEntity);
                authToReturn.AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken);

                return Tuple.Create(authToReturn, userEntity.RefreshToken);
            }
            return null;
        }
    }
}