using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models.DTO.Admin;
using server.Services;

namespace server.Controllers.Admin
{
    [ApiController]
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
        public async Task<ActionResult<AuthOutDto>> LoginAsync(AuthInDto userAuth)
        {
            if (ModelState.IsValid) 
            {
                var authToReturn = await _authServive.LoginAsync(userAuth);
                if (authToReturn != null)
                    return Ok(authToReturn);

                return Unauthorized();
            }

            return BadRequest("Invalid credentials");
        }
    }
}