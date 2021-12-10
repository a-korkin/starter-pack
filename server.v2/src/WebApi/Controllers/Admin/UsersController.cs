using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Common.Models.DTO.Admin;
using Application.Features.Admin.Users;
using System.Collections.Generic;

namespace WebApi.Controllers.Admin
{
    [ApiController]
    [Route("/api/admin/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator ??
                throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserOutDto>>> GetAllAsync()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserOutDto>> GetByIdAsync(Guid id)
        {
            var user = await _mediator.Send(new GetByIdUserQuery { Id = id });
            if (user != null)
                return Ok(user);
            
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<UserOutDto>> CreateAsync(UserInDto user)
        {
            var userToReturn = await _mediator.Send(new CreateUserCommand { UserIn = user});
            
            return CreatedAtRoute(
                "GetUser",
                new { id = userToReturn.Id },
                userToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (await _mediator.Send(new DeleteUserCommand { Id = id })) 
                return NoContent();
            
            return NotFound();
        }
    }
}