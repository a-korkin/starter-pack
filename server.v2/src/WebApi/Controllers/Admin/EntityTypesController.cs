using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Attributes;
using Domain.Entities.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers.Admin
{
    [ApiController]
    [Route("/api/admin/entity-types")]
    public class EntityTypesController : ControllerBase
    {
        private readonly IApplicationDbContext _context;

        public EntityTypesController(IApplicationDbContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAsync()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var types = assemblies
                .SelectMany(s => s.GetTypes().Where(w => w.FullName.Contains("Entities")));

            var existingEntityTypes = await _context.EntityTypes.ToListAsync();

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

                        await _context.EntityTypes.AddAsync(newEntity);
                        await _context.SaveChangesAsync();
                    } 
                }
            }

            return Ok(existingEntityTypes);
        }
    }
}