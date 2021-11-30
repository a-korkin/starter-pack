using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Controllers.Base;
using server.Entities.Admin;
using server.Models.DTO.Admin;
using server.Repositories;

namespace server.Controllers.Admin
{
    // [ApiController]
    [Route("api/admin/roles/{roleId}/claims")]
    public class ClaimsController : BaseController
    {
        public ClaimsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) {}
        // {
        //     _repository = repository ??
        //         throw new ArgumentNullException(nameof(repository));

        //     _mapper = mapper ??
        //         throw new ArgumentNullException(nameof(mapper));
        // }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaimOutDto>>> GetAllAsync(Guid roleId) 
        {
            var itemsFromRepo = await _unitOfWork.Claims.GetAllByAsync(w => w.RoleId == roleId);
            return Ok(_mapper.Map<IEnumerable<ClaimOutDto>>(itemsFromRepo));
        }

        [HttpGet("{itemId}", Name = "GetClaim")]
        public async Task<ActionResult<ClaimOutDto>> GetByIdAsync(Guid roleId, Guid itemId)
        {
            var itemFromRepo = await _unitOfWork.Claims.GetByIdAsync(itemId);
            if (itemFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<ClaimOutDto>(itemFromRepo));
        }

        [HttpPost]
        public async Task<ActionResult<ClaimOutDto>> CreateAsync(Guid roleId, ClaimInDto item)
        {
            var claimExists = await _unitOfWork.Claims.ExistsByExpAsync(w => w.RoleId == roleId && w.TypeId == item.TypeId);
            if (claimExists) 
                return BadRequest("Claim already exists");

            var itemEntity = _mapper.Map<Claim>(item);
            itemEntity.RoleId = roleId;
            // await _repository.AddAsync(itemEntity);
            // await _repository.SaveAsync();

            await _unitOfWork.Claims.AddAsync(itemEntity);
            await _unitOfWork.CompleteAsync();

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
            if (await _unitOfWork.Claims.ExistsByIdAsync(itemId))
            {
                var claimEntity = await _unitOfWork.Claims.GetByIdAsync(itemId);
                _mapper.Map(item, claimEntity);
                _unitOfWork.Claims.Update(claimEntity);
                // await _repository.SaveAsync();
                await _unitOfWork.CompleteAsync();

                var claimToReturn = _mapper.Map<ClaimOutDto>(claimEntity);
                return Ok(claimToReturn);
            }

            return NotFound("Claim not found");
        }
    }
}