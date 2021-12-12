using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.DTO.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin
{
    [Route("/api")]
    public class AuthenticationsController : ApiControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationsController(IAuthService authService)
        {
            _authService = authService ??
                throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthOutDto>> LoginAsync(AuthInDto userAuth)
        {
            if (ModelState.IsValid) 
            {
                var authToReturn = await _authService.LoginAsync(userAuth);
                if (authToReturn != null)
                {
                    Response.Cookies.Append("refreshToken", authToReturn.Item2, new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = DateTime.UtcNow.AddDays(7),             
                    });
                    
                    return Ok(authToReturn.Item1);
                }

                return Unauthorized();
            }

            return BadRequest("Invalid credentials");
        }

        [HttpPost("logout")]
        public async Task<ActionResult<AuthOutDto>> LogoutAsync()
        {
            if (HttpContext.User.HasClaim(c => c.Type == "id"))
            {
                var userId = HttpContext.User.Claims
                    .Where(w => w.Type == "id")
                    .Select(s => Guid.Parse(s.Value))
                    .FirstOrDefault();
                
                Response.Cookies.Delete("refreshToken");

                var authToReturn = await _authService.LogoutAsync(userId);
                return Ok(authToReturn);
            }
            return Unauthorized();
        }
    }
}