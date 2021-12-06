using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Features.UserFeatures.Queries;
using Application.Features.UserFeatures.Commands;
using Application.Common.Models.DTO.Admin;

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
        // public async Task<IActionResult> CreateAsync(CreateUserCommand command)
        {
            // var user = await _mediator.Send(command);
            var command = new CreateUserCommand
            {
                UserIn = user
            };
            var userToReturn = await _mediator.Send(command);
            
            return Ok(userToReturn);
        }
    }
}