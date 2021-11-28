using System;
using System.Collections.Generic;
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
using server.Repositories;
using SystemClaims = System.Security.Claims;

namespace server.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;

        public AuthService( 
            IConfiguration configuration, 
            IMapper mapper,
            IUserRepository repository)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
            
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
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
            var userEntity = await _repository.GetOneByAsync(x => x.UserName == userAuth.UserName);
                
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

                await _repository.SaveAsync();

                return Tuple.Create(authToReturn, userEntity.RefreshToken);
            }
            return null;
        }

        public async Task<AuthOutDto> LogoutAsync(Guid userId)
        {
            var userEntity = await _repository.GetByIdAsync(userId);
            
            if (userEntity != null)
            {
                userEntity.RefreshToken = null;
                await _repository.SaveAsync();

                var authToReturn = _mapper.Map<AuthOutDto>(userEntity);
                authToReturn.AccessToken = null;

                return authToReturn;
            }
            return null;
        }
    }
}