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
    [Route("api/admin/roles/{roleId}/claims")]
    public class ClaimsController : ControllerBase
    {
        private readonly IBaseRepository<Claim> _repository;
        private readonly IMapper _mapper;

        public ClaimsController(
            IBaseRepository<Claim> repository, 
            IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaimOutDto>>> GetAllAsync(Guid roleId) 
        {
            var itemsFromRepo = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ClaimOutDto>>(itemsFromRepo));
        }

        [HttpGet("{itemId}", Name = "GetClaim")]
        public async Task<ActionResult<ClaimOutDto>> GetByIdAsync(Guid roleId, Guid itemId)
        {
            var itemFromRepo = await _repository.GetByIdAsync(itemId);
            if (itemFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<ClaimOutDto>(itemFromRepo));
        }

        [HttpPost]
        public async Task<ActionResult<ClaimOutDto>> CreateAsync(Guid roleId, ClaimInDto item)
        {
            var itemEntity = _mapper.Map<Claim>(item);
            itemEntity.RoleId = roleId;
            await _repository.AddAsync(itemEntity);
            await _repository.SaveAsync();

            var itemToReturn = _mapper.Map<ClaimOutDto>(itemEntity);

            return CreatedAtRoute("GetClaim",
                new { roleId = roleId, itemId = itemToReturn.Id },
                itemToReturn);
        }

        [HttpPut("{itemId}")]
        public async Task<ActionResult<ClaimOutDto>> UpdateAsync(
            Guid roleId,
            Guid itemId, 
            ClaimUpdDto item)
        {
            if (await _repository.ExistsByIdAsync(itemId))
            {
                var claimEntity = await _repository.GetByIdAsync(itemId);
                _mapper.Map(item, claimEntity);
                _repository.Update(claimEntity);
                await _repository.SaveAsync();

                var claimToReturn = _mapper.Map<ClaimOutDto>(claimEntity);
                return Ok(claimToReturn);
            }

            return NotFound("Claim not found");
        }
    }
}