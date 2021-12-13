using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Models.DTO.Admin;
using Application.Features.Admin.Users;
using System.Collections.Generic;

namespace WebApi.Controllers.Admin
{
    [Route("/api/admin/users")]
    public class UsersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserOutDto>>> GetAllAsync()
        {
            var users = await Mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserOutDto>> GetByIdAsync(Guid id)
        {
            var user = await Mediator.Send(new GetByIdUserQuery { Id = id });
            if (user != null)
                return Ok(user);
            
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<UserOutDto>> CreateAsync(UserInDto user)
        {
            var userToReturn = await Mediator.Send(new CreateUserCommand { UserIn = user});
            
            return CreatedAtRoute(
                "GetUser",
                new { id = userToReturn.Id },
                userToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (await Mediator.Send(new DeleteUserCommand { Id = id })) 
                return NoContent();
            
            return NotFound();
        }
    }
}