using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Models.DTO.Admin;
using Application.Features.Admin.Roles;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.Admin
{
    [Route("/api/admin/roles/{roleId}/claims")]
    public class ClaimsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaimOutDto>>> GetAllAsync(Guid roleId) 
        {
            var claims = await Mediator.Send(new GetAllClaimsQuery { RoleId = roleId });
            return Ok(claims);
        }

        [HttpGet("{claimId}", Name = "GetClaim")]
        public async Task<ActionResult<ClaimOutDto>> GetByIdAsync(Guid roleId, Guid claimId)
        {
            var claim = await Mediator.Send(new GetByIdClaimQuery { RoleId = roleId, ClaimId = claimId });
            if (claim == null)
                return NotFound();

            return Ok(claim);
        }

        [HttpPost]
        public async Task<ActionResult<ClaimOutDto>> CreateAsync(Guid roleId, ClaimInDto item)
        {
            var itemToReturn = await Mediator.Send(new CreateClaimCommand { RoleId = roleId, ClaimIn = item });

            return CreatedAtRoute("GetClaim",
                new { roleId = roleId, claimId = itemToReturn.Id },
                itemToReturn);
        }

        // [HttpPut("{itemId}")]
        // public async Task<ActionResult<ClaimOutDto>> UpdateAsync(
        //     Guid roleId,
        //     Guid itemId, 
        //     ClaimUpdDto item)
        // {
        //     if (await _unitOfWork.Repository<Claim>().ExistsByIdAsync(itemId))
        //     {
        //         var claimEntity = await _unitOfWork.Repository<Claim>().GetByIdAsync(itemId);
        //         _mapper.Map(item, claimEntity);
        //         await _unitOfWork.CompleteAsync();

        //         var claimToReturn = _mapper.Map<ClaimOutDto>(claimEntity);
        //         return Ok(claimToReturn);
        //     }

        //     return NotFound("Claim not found");
        // }
    }
}