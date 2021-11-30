using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Controllers.Base;
using server.Entities.Common;
using server.Models.DTO.Common;
using server.Repositories;

namespace server.Controllers.Common 
{
    // [ApiController]
    [Route("/api/common/persons")]
    // [Authorize(Policy = "ClaimsRequired")]
    public class PersonsController : BaseController //ControllerBase 
    {
    //     private readonly IGenericRepository<Person> _repository;
        // private readonly IUnitOfWork _unitOfWork;
        // private readonly IMapper _mapper;

        public PersonsController(
            // IGenericRepository<Person> repository, 
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper) {}
        // {
        //     _repository = repository ?? 
        //         throw new ArgumentNullException(nameof(repository));

        //     _mapper = mapper ??
        //         throw new ArgumentNullException(nameof(mapper));
        // }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonOutDto>>> GetAllAsync() 
        {
            // var personsFromRepo = await _repository.GetAllAsync();
            var personsFromRepo = await _unitOfWork.Persons.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<PersonOutDto>>(personsFromRepo));
        }

        [HttpGet("{personId}", Name = "GetPerson")]        
        public async Task<ActionResult<PersonOutDto>> GetByIdAsync(Guid personId) 
        {
            // var personEntity = await _repository.GetByIdAsync(personId);
            var personEntity = await _unitOfWork.Persons.GetByIdAsync(personId);

            if (personEntity == null)
                return NotFound();

            return Ok(_mapper.Map<PersonOutDto>(personEntity));
        }

        [HttpPost]
        public async Task<ActionResult<PersonOutDto>> CreateAsync(PersonInDto item)
        {
            var personEntity = _mapper.Map<Person>(item);
            // await _repository.AddAsync(personEntity);
            // await _repository.SaveAsync();
            await _unitOfWork.Persons.AddAsync(personEntity);
            await _unitOfWork.CompleteAsync();

            var personToReturn = _mapper.Map<PersonOutDto>(personEntity);

            return CreatedAtRoute("GetPerson", 
                new { personId = personToReturn.Id },
                personToReturn);
        }

        [HttpDelete("{personId}")]
        public async Task<IActionResult> DeleteAsync(Guid personId)
        {
            // if (await _repository.DeleteAsync(personId))
            if (await _unitOfWork.Persons.DeleteAsync(personId))
                return NoContent();

            return NotFound();
        }
    }
}