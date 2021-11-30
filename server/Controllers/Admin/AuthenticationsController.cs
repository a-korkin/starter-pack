using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using server.Models.DTO.Admin;
using server.Services;

namespace server.Controllers.Admin
{
    [ApiController]
    [Authorize(Policy = "ClaimsRequired")]
    [Route("/api")]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthService _authServive;

        public AuthenticationsController(IAuthService authService)
        {
            _authServive = authService ??
                throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthOutDto>> LoginAsync(AuthInDto userAuth)
        {
            if (ModelState.IsValid) 
            {
                var authToReturn = await _authServive.LoginAsync(userAuth);
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

        [HttpPost]
        [Route("logout")]
        public async Task<ActionResult<AuthOutDto>> LogoutAsync()
        {
            if (HttpContext.User.HasClaim(c => c.Type == "id"))
            {
                var userId = HttpContext.User.Claims
                    .Where(w => w.Type == "id")
                    .Select(s => Guid.Parse(s.Value))
                    .FirstOrDefault();
                
                Response.Cookies.Delete("refreshToken");

                var authToReturn = await _authServive.LogoutAsync(userId);
                return Ok(authToReturn);
            }
            return Unauthorized();
        }
    }
}