using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Entities.Common;
using server.Models;
using server.Services;

namespace server.Controllers 
{
    [ApiController]
    [Route("/api/persons")]
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
        public ActionResult<IEnumerable<PersonDto>> GetAll() 
        {
            var personsFromRepo = _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<PersonDto>>(personsFromRepo));
        }

        [HttpGet("{personId}", Name = "GetPerson")]
        public ActionResult<PersonDto> GetById(Guid personId) 
        {
            var personEntity = _repository.GetById(personId);

            if (personEntity == null)
                return NotFound();

            return Ok(_mapper.Map<PersonDto>(personEntity));
        }

        [HttpPost]
        public ActionResult<PersonDto> Create(PersonCreateDto item)
        {
            Guid id = Guid.NewGuid();
            var personEntity = _mapper.Map<Person>(item);
            personEntity.Id = id;
            _repository.Add(personEntity);
            _repository.Save();

            var personToReturn = _mapper.Map<PersonDto>(personEntity);

            return CreatedAtRoute("GetPerson", 
                new { personId = personToReturn.Id },
                personToReturn);
        }

    }
}