using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
        public async Task<ActionResult<IEnumerable<EntityTypeOutDto>>> GetAll()
        {
            var itemsFromRepo = await _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<EntityTypeOutDto>>(itemsFromRepo));
        }

        [HttpGet("{itemId}", Name = "GetEntityType")]
        public async Task<ActionResult<EntityTypeOutDto>> GetById(Guid itemId)
        {
            var itemFromRepo = await _repository.GetById(itemId);
            if (itemFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<EntityTypeOutDto>(itemFromRepo));
        }

        [HttpPost]
        public async Task<ActionResult<EntityTypeOutDto>> Create(EntityTypeInDto item)
        {
            var itemEntity = _mapper.Map<EntityType>(item);
            await _repository.Add(itemEntity);

            var itemToReturn = _mapper.Map<EntityTypeOutDto>(itemEntity);

            return CreatedAtRoute("GetEntityType",
                new { itemId = itemToReturn.Id },
                itemToReturn);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(w => w.FullName.Contains("server.Entities"))
                .Where(w => !new string[] { "EntityType", "BaseModel" }.Contains(w.Name));

            var existingEntityTypes = await _repository.GetAll();                

            foreach (var type in types) 
            {
                DescriptionAttribute attribute =
                    (DescriptionAttribute)Attribute.GetCustomAttribute(type, typeof(DescriptionAttribute));

                if (attribute != null)
                {
                    bool entityExists = existingEntityTypes
                        .Any(a => a.Schema == attribute.Schema && a.TableName == attribute.TableName);

                    if (!entityExists) 
                    {
                        var newEntity = new EntityType 
                        {
                            Name = attribute.Name,
                            Slug = attribute.Slug,
                            Schema = attribute.Schema,
                            TableName = attribute.TableName
                        };

                        await _repository.Add(newEntity);
                    }                   
                }
            }

            await _repository.Save();

            return Ok();
        }
    }
}
