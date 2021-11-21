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

        [HttpPost]
        public ActionResult<PersonDto> Create(PersonCreateDto item)
        {
            // var entity = _repository.Create();
            // return Created();
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<PersonDto>> GetAll() 
        {
            var personsFromRepo = _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<PersonDto>>(personsFromRepo));
        }
    }
}