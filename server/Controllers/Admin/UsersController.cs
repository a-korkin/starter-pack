using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Entities.Admin;
using server.Models.DTO.Admin;
using server.Services;

namespace server.Controllers.Admin 
{
    [ApiController]
    [Route("/api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IBaseRepository<User> _repository;
        private readonly IMapper _mapper;

        public UsersController(IBaseRepository<User> repository, IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));                
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserOutDto>>> GetAllAsync()
        {
            var userEntities = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<UserOutDto>>(userEntities));
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<ActionResult<UserOutDto>> GetByIdAsync(Guid userId)
        {
            var userEntity = await _repository.GetByIdAsync(userId);

            if (userEntity == null)
                return NotFound();

            return Ok(_mapper.Map<UserOutDto>(userEntity));
        }

        [HttpPost]
        public async Task<ActionResult<UserOutDto>> CreateAsync(UserInDto user)
        {
            var userEntity = _mapper.Map<User>(user);
            await _repository.AddAsync(userEntity);
            await _repository.SaveAsync();

            var userToReturn = _mapper.Map<UserOutDto>(userEntity);

            return CreatedAtRoute("GetUser",
                new { userId = userToReturn.Id },
                userToReturn);
        }
    }
}