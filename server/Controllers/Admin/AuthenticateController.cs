using System;
using Microsoft.AspNetCore.Mvc;
using server.Entities.Admin;
using server.Services;

namespace server.Controllers.Admin
{
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IBaseRepository<User> _repository;

        public AuthenticateController(IBaseRepository<User> repository)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));
        }
    }
}