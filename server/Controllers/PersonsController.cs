using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Attributes;
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
            // var ass = Assembly.GetExecutingAssembly().GetTypes().Where(w => w.FullName.Contains("server.Entities"));
            // foreach (var d in ass) 
            // {
            //     Console.WriteLine($"{d}");
            //     GetAttribute(d);
            // }

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

        // public void GetAttribute(Type t)
        // {
        //     // Get instance of the attribute.
        //     FuckAttribute MyAttribute =
        //         (FuckAttribute) Attribute.GetCustomAttribute(t, typeof (FuckAttribute));

        //     if (MyAttribute == null)
        //     {
        //         Console.WriteLine("The attribute was not found.");
        //     }
        //     else
        //     {
        //         // Get the Name value.
        //         Console.WriteLine("The Name Attribute is: {0}." , MyAttribute.Name);
        //     }
        // }
    }
}