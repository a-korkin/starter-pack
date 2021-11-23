using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using server.Entities.Admin;
using server.Models.DTO.Admin;
using server.Services;

namespace server.Controllers.Admin
{
    [ApiController]
    [Route("/api")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IBaseRepository<User> _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IBaseRepository<User> repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));                

            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserAuthDto userAuth)
        {
            if (ModelState.IsValid) 
            {
                var userEntity = await _repository.GetByUserNameAsync(userAuth.UserName);
                
                //TODO change
                if (userEntity != null && 
                    userEntity.Password == userAuth.Password)
                {
                    var claims = new Claim[] 
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("id", userEntity.Id.ToString()),
                        new Claim("userName", userEntity.UserName)
                    }; 

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken
                    (
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(15),
                        signingCredentials: signIn
                    );

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }

                return Unauthorized();
            }

            return BadRequest("Invalid credentials");
        }
    }
}