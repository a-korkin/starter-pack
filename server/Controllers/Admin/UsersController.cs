using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Entities.Admin;
using server.Models.DTO.Admin;
using server.Repositories;

namespace server.Controllers.Admin 
{
    [ApiController]
    [Route("/api/admin/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UsersController(
            IUserRepository repository, 
            IMapper mapper)
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

        [HttpGet("{itemId}", Name = "GetUser")]
        public async Task<ActionResult<UserOutDto>> GetByIdAsync(Guid itemId)
        {
            var userEntity = await _repository.GetByIdAsync(itemId);

            if (userEntity == null)
                return NotFound();

            return Ok(_mapper.Map<UserOutDto>(userEntity));
        }

        [HttpPost]
        public async Task<ActionResult<UserOutDto>> CreateAsync(UserInDto user)
        {
            var users = await _repository.GetAllByAsync(u => u.UserName == user.UserName);
            if (users.Any())
            {
                return BadRequest("User already exists");
            }

            var userEntity = _mapper.Map<User>(user);
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            userEntity.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
            await _repository.AddAsync(userEntity);
            await _repository.SaveAsync();

            var userToReturn = _mapper.Map<UserOutDto>(userEntity);

            return CreatedAtRoute("GetUser",
                new { itemId = userToReturn.Id },
                userToReturn);
        }

        [HttpPut("{itemId}")]
        public async Task<ActionResult<UserOutDto>> UpdateAsync(
            Guid itemId, 
            UserUpdDto item)
        {
            if (await _repository.ExistsByIdAsync(itemId))
            {
                var userEntity = await _repository.GetByIdAsync(itemId);
                _mapper.Map(item, userEntity);
                _repository.Update(userEntity);
                await _repository.SaveAsync();

                var userToReturn = _mapper.Map<UserOutDto>(userEntity);
                return Ok(userToReturn);
            }

            return NotFound("User not found");
        }

        [HttpDelete]
        [Route("{itemId}")]
        public async Task<IActionResult> DeleteAsync(Guid itemId)
        {
            if (await _repository.DeleteAsync(itemId))
                return NoContent();

            return NotFound();
        }
    }
}