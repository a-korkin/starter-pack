using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Entities.Admin;
using server.Models.DTO.Admin;
using server.Repositories;

namespace server.Controllers.Admin
{
    [ApiController]
    [Route("/api/admin/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IBaseRepository<Role> _repository;
        private readonly IMapper _mapper;
        public RolesController(
            IBaseRepository<Role> repository, 
            IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }   

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleOutDto>>> GetAllAsync()
        {
            var roleEntities = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RoleOutDto>>(roleEntities));
        }

        [HttpGet("{itemId}", Name = "GetRole")]
        public async Task<ActionResult<RoleOutDto>> GetByIdAsync(Guid itemId)
        {
            var entity = await _repository.GetByIdAsync(itemId);

            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<RoleOutDto>(entity));
        }

        [HttpPost]
        public async Task<ActionResult<RoleOutDto>> CreateAsync(RoleInDto item)
        {
            if (await _repository.ExistsByExpAsync(w => w.Title == item.Title))
                return BadRequest("Role already exists");

            var entity = _mapper.Map<Role>(item);
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            var entityToReturn = _mapper.Map<RoleOutDto>(entity);
            
            return CreatedAtRoute("GetRole",
                new { itemId = entityToReturn.Id },
                entityToReturn);
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteAsync(Guid itemId)
        {
            if (await _repository.DeleteAsync(itemId))
                return NoContent();
            
            return NotFound();
        }
    }
}