using System;
using System.Collections.Generic;
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

        public PersonsController(IBaseRepository<Person> repository)
        {
            _repository = repository ?? 
                throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        public ActionResult<PersonDto> Create(PersonDto item)
        {
            // var entity = _repository.Create();
            // return Created();
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAll() 
        {
            var list = _repository.GetAll();
            return Ok(list);
        }
    }
}