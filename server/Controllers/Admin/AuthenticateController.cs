using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Entities.Admin;
using server.Services;

namespace server.Controllers.Admin
{
    [ApiController]
    [Route("/api/authenticate")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IBaseRepository<User> _repository;
        private readonly IMapper _mapper;

        public AuthenticateController(IBaseRepository<User> repository, IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));                
        }
    }
}