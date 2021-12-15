using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.Common.Models.DTO.Admin;
using Application.Features.Admin.Users;
using System.Collections.Generic;
using Application.Common.Models.Helpers;

namespace WebApi.Controllers.Admin
{
    [Route("/api/admin/users")]
    public class UsersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedList<UserOutDto>>> GetAllAsync([FromQuery] GetUsersQuery query)
        {
            var users = await Mediator.Send(query);
            return Ok(users);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserOutDto>> GetByIdAsync([FromRoute] GetByIdUserQuery query)
        {
            var user = await Mediator.Send(query);
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