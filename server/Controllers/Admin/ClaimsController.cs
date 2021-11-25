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
    [Route("api/claims")]
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
        public async Task<ActionResult<IEnumerable<ClaimOutDto>>> GetAllAsync() 
        {
            var itemsFromRepo = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ClaimOutDto>>(itemsFromRepo));
        }

        [HttpGet("{itemId}", Name = "GetClaim")]
        public async Task<ActionResult<ClaimOutDto>> GetByIdAsync(Guid itemId)
        {
            var itemFromRepo = await _repository.GetByIdAsync(itemId);
            if (itemFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<ClaimOutDto>(itemFromRepo));
        }

        [HttpPost]
        public async Task<ActionResult<ClaimOutDto>> CreateAsync(ClaimInDto item)
        {
            var itemEntity = _mapper.Map<Claim>(item);
            await _repository.AddAsync(itemEntity);
            await _repository.SaveAsync();

            var itemToReturn = _mapper.Map<ClaimOutDto>(itemEntity);

            return CreatedAtRoute("GetClaim",
                new { itemId = itemToReturn.Id },
                itemToReturn);
        }
    }
}