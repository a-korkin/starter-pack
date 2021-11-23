using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Entities.Common;
using server.Models.DTO.Common;
using server.Services;

namespace server.Controllers.Common 
{
    [ApiController]
    [Route("/api/persons")]
    [Authorize]
    public class PersonsController : ControllerBase 
    {
        private readonly IBaseRepository<Person> _repository;
        private readonly IMapper _mapper;

        public PersonsController(IBaseRepository<Person> repository, IMapper mapper)
        {
            _repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonOutDto>>> GetAllAsync() 
        {
            var personsFromRepo = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PersonOutDto>>(personsFromRepo));
        }

        [HttpGet("{personId}", Name = "GetPerson")]
        public async Task<ActionResult<PersonOutDto>> GetByIdAsync(Guid personId) 
        {
            var personEntity = await _repository.GetByIdAsync(personId);

            if (personEntity == null)
                return NotFound();

            return Ok(_mapper.Map<PersonOutDto>(personEntity));
        }

        [HttpPost]
        public async Task<ActionResult<PersonOutDto>> CreateAsync(PersonInDto item)
        {
            var personEntity = _mapper.Map<Person>(item);
            await _repository.AddAsync(personEntity);
            await _repository.SaveAsync();

            var personToReturn = _mapper.Map<PersonOutDto>(personEntity);

            return CreatedAtRoute("GetPerson", 
                new { personId = personToReturn.Id },
                personToReturn);
        }
    }
}