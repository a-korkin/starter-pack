using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Attributes;
using server.Entities.Admin;
using server.Models.Admin;
using server.Services;

namespace server.Controllers.Admin
{
    [ApiController]
    [Route("/api/entity_types")]
    public class EntityTypesController : ControllerBase
    {
        private readonly IBaseRepository<EntityType> _repository;
        private readonly IMapper _mapper;

        public EntityTypesController(IBaseRepository<EntityType> repository, IMapper mapper)
        {
            _repository = repository ??
                throw new ArgumentNullException(nameof(repository));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public ActionResult<IEnumerable<EntityTypeOutDto>> GetAll()
        {
            var itemsFromRepo = _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<EntityTypeOutDto>>(itemsFromRepo));
        }

        [HttpGet("{itemId}", Name = "GetEntityType")]
        public ActionResult<EntityTypeOutDto> GetById(Guid itemId)
        {
            var itemFromRepo = _repository.GetById(itemId);
            if (itemFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<EntityTypeOutDto>(itemFromRepo));
        }

        [HttpPost]
        public ActionResult<EntityTypeOutDto> Create(EntityTypeInDto item)
        {
            var itemEntity = _mapper.Map<EntityType>(item);
            _repository.AddType(itemEntity);

            var itemToReturn = _mapper.Map<EntityTypeOutDto>(itemEntity);

            return CreatedAtRoute("GetEntityType",
                new { itemId = itemToReturn.Id },
                itemToReturn);
        }

        [HttpPost("/refresh")]
        public IActionResult CreateAll()
        {
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(w => w.FullName.Contains("server.Entities"))
                .Where(w => !new string[] { "EntityType", "BaseModel" }.Contains(w.Name));

            foreach (var type in types) 
            {
                DescriptionAttribute attribute =
                    (DescriptionAttribute)Attribute.GetCustomAttribute(type, typeof(DescriptionAttribute));

                if (attribute != null)
                    Console.WriteLine($"name: {attribute.Name} slug: {attribute.Slug}");
            }

            return Ok();
        }
    }
}
