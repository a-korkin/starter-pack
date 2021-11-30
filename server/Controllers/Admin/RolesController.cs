using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using server.Controllers.Base;
using server.Entities.Admin;
using server.Models.DTO.Admin;
using server.Repositories;

namespace server.Controllers.Admin
{
    // [ApiController]
    [Route("/api/admin/roles")]
    public class RolesController : BaseController //ControllerBase
    {
        // private readonly IRoleRepository _repository;
        // private readonly IUnitOfWork _unitOfWork;
        // private readonly IMapper _mapper;
        public RolesController(
            // IRoleRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper) {}
        // {
        //     _repository = repository ??
        //         throw new ArgumentNullException(nameof(repository));

        //     _mapper = mapper ??
        //         throw new ArgumentNullException(nameof(mapper));
        // }   

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleOutDto>>> GetAllAsync()
        {
            // var roleEntities = await _repository.GetAllAsync();
            var roleEntities = await _unitOfWork.Roles.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RoleOutDto>>(roleEntities));
        }

        [HttpGet("{itemId}", Name = "GetRole")]
        public async Task<ActionResult<RoleOutItemDto>> GetByIdAsync(Guid itemId)
        {
            var entity = await _unitOfWork.Roles.GetRoleWithChildren(itemId);

            if (entity == null)
                return NotFound();

            return Ok(_mapper.Map<RoleOutItemDto>(entity));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromBody] RoleInDto item,
            [FromServices] IOptions<ApiBehaviorOptions> apiBehaviorOptions)
        {
            if (await _unitOfWork.Roles.ExistsByExpAsync(w => w.Title == item.Title))
            {
                ModelState.AddModelError(nameof(RoleInDto), $"Роль: {item.Title} уже существует");
                return apiBehaviorOptions.Value.InvalidModelStateResponseFactory(ControllerContext);
            }

            var entity = _mapper.Map<Role>(item);
            // await _repository.AddAsync(entity);
            // await _repository.SaveAsync();
            await _unitOfWork.Roles.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            
            // entity = await _repository.GetRoleWithChildren(entity.Id);
            entity = await _unitOfWork.Roles.GetRoleWithChildren(entity.Id);

            var entityToReturn = _mapper.Map<RoleOutItemDto>(entity);

            return CreatedAtRoute("GetRole",
                new { itemId = entityToReturn.Id },
                entityToReturn);
        }

        [HttpDelete("{itemId}")]
        public async Task<IActionResult> DeleteAsync(Guid itemId)
        {
            if (await _unitOfWork.Roles.DeleteAsync(itemId))
                return NoContent();
            
            return NotFound();
        }
    }
}