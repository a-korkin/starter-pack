using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interface;
using Application.Common.Models.DTO.Admin;
using Application.Common.Profiles.Admin;
using AutoMapper;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SystemClaims = System.Security.Claims;

namespace Infrastructure.Services
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

        private string CreateToken(
            IEnumerable<SystemClaims.Claim> claims,
            DateTime expires,
            SymmetricSecurityKey key)
        {
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: signIn
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<Tuple<AuthOutDto, string>> LoginAsync(AuthInDto userAuth)
        {
            var userEntity = await _context.Users
                .Where(w => w.UserName == userAuth.UserName)
                .FirstOrDefaultAsync();
            
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

                var accessKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:AccessKey"]));
                var accessToken = CreateToken(claims: claims, expires: DateTime.Now.AddMinutes(15), key: accessKey);
                var authToReturn = _mapper.Map<AuthOutDto>(userEntity);
                authToReturn.AccessToken = accessToken;

                var refreshKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:RefreshKey"]));
                var refreshToken = CreateToken(claims: claims, expires: DateTime.Now.AddDays(7), key: refreshKey);
                userEntity.RefreshToken = refreshToken;

                return Tuple.Create(authToReturn, userEntity.RefreshToken);                
            }
            return null;
        }

        public async Task<AuthOutDto> LogoutAsync(Guid userId)
        {
            var userEntity = await _context.Users
                .SingleOrDefaultAsync(a => a.Id == userId);
            
            if (userEntity != null)
            {
                userEntity.RefreshToken = null;

                var authToReturn = _mapper.Map<AuthOutDto>(userEntity);
                authToReturn.AccessToken = null;

                return authToReturn;
            }
            return null;
        }
    }
}