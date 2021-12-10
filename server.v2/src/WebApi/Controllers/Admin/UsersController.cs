using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Common.Models.DTO.Admin;
using Application.Features.Admin.Users;

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
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserInDto user)
        {
            var userToReturn = await _mediator.Send(new CreateUserCommand { UserIn = user});
            
            return Ok(userToReturn);
        }
    }
}