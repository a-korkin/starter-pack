using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using server.Attributes;
using server.Controllers.Base;
using server.Entities.Admin;
using server.Models.DTO.Admin;
using server.Repositories;

namespace server.Controllers.Admin
{
    [Route("/api/admin/entity-types")]
    public class EntityTypesController : BaseController
    {
        public EntityTypesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) {}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntityTypeOutDto>>> GetAllAsync()
        {
            var itemsFromRepo = await _unitOfWork.EntityTypes.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<EntityTypeOutDto>>(itemsFromRepo));
        }

        [HttpGet("{itemId}", Name = "GetEntityType")]
        public async Task<ActionResult<EntityTypeOutDto>> GetByIdAsync(Guid itemId)
        {
            var itemFromRepo = await _unitOfWork.EntityTypes.GetByIdAsync(itemId);
            if (itemFromRepo == null)
                return NotFound();

            return Ok(_mapper.Map<EntityTypeOutDto>(itemFromRepo));
        }

        [HttpPost]
        public async Task<ActionResult<EntityTypeOutDto>> CreateAsync(EntityTypeInDto item)
        {
            var itemEntity = _mapper.Map<EntityType>(item);

            await _unitOfWork.EntityTypes.AddAsync(itemEntity);
            await _unitOfWork.CompleteAsync();

            var itemToReturn = _mapper.Map<EntityTypeOutDto>(itemEntity);

            return CreatedAtRoute("GetEntityType",
                new { itemId = itemToReturn.Id },
                itemToReturn);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> RefreshAsync()
        {
            var types = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(w => w.FullName.Contains("server.Entities"))
                .Where(w => !new string[] { "EntityType", "BaseModel" }.Contains(w.Name));

            var existingEntityTypes = await _unitOfWork.EntityTypes.GetAllAsync();                

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

                        await _unitOfWork.EntityTypes.AddAsync(newEntity);
                    }                   
                }
            }

            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
