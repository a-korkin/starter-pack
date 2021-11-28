using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using server.Entities.Admin;
using server.Models.DTO.Admin;
using server.Repositories;

namespace server.Controllers.Admin
{
    [ApiController]
    [Route("/api/admin/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _repository;
        private readonly IMapper _mapper;
        public RolesController(
            IRoleRepository repository,
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
        public async Task<ActionResult<RoleOutItemDto>> GetByIdAsync(Guid itemId)
        {
            var entity = await _repository.GetRoleWithChildren(itemId);

            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<RoleOutItemDto>(entity));
        }

        // public async Task<ActionResult<RoleOutItemDto>> CreateAsync(
        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromBody] RoleInDto item,
            [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            if (await _repository.ExistsByExpAsync(w => w.Title == item.Title))
            {
                ModelState.AddModelError(nameof(RoleInDto), $"Роль: {item.Title} уже существует");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }

            var entity = _mapper.Map<Role>(item);
            await _repository.AddAsync(entity);
            await _repository.SaveAsync();
            
            entity = await _repository.GetRoleWithChildren(entity.Id);

            var entityToReturn = _mapper.Map<RoleOutItemDto>(entity);

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