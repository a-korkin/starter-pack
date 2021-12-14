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
            var claim = await Mediator.Send(new CreateClaimCommand { RoleId = roleId, ClaimIn = item });

            return CreatedAtRoute("GetClaim",
                new { roleId = roleId, claimId = claim.Id },
                claim);
        }

        [HttpPut("{claimId}")]
        public async Task<ActionResult<ClaimOutDto>> UpdateAsync(
            Guid roleId,
            Guid claimId, 
            ClaimUpdDto item)
        {
            var claim = await Mediator.Send(new UpdateClaimCommand { RoleId = roleId, ClaimId = claimId, ClaimUpd = item });
            if (claim == null)
                return NotFound();

            return Ok(claim);
        }
    }
}