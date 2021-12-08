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
            // var types = Assembly
            //     .GetExecutingAssembly()
            //     .GetTypes();
                // .Where(w => w.FullName.Contains("server.Entities"))
                // .Where(w => !new string[] { "EntityType", "BaseModel" }.Contains(w.Name));

            // var types = AppDomain.CurrentDomain.GetAssemblies()
            //     .Where(w => w.FullName.Contains("Entities"))
            //     .SelectMany(s => s.GetTypes());

            // foreach (var item in types)
            // {
            //     Console.WriteLine(item);
            // }

            // System.Reflection.Assembly ass = System.Reflection.Assembly.GetEntryAssembly();

            // foreach (System.Reflection.TypeInfo ti in ass.DefinedTypes)
            // {
            //     if (ti.ImplementedInterfaces.Contains(typeof()))
            //     {
            //         ass.CreateInstance(ti.FullName) as yourInterface;
            //     }  
            // }


            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var existingEntityTypes = await _context.EntityTypes.ToListAsync();

            foreach (var ass in assemblies)
            {
                foreach (var type in ass.GetTypes().Where(w => w.FullName.Contains("Entities")))
                {
                    DescriptionAttribute attribute =
                        (DescriptionAttribute)Attribute.GetCustomAttribute(type, typeof(DescriptionAttribute));

                    if (attribute != null)
                    {
                        Console.WriteLine($"{ass.FullName}: {type}");
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
                            // await _unitOfWork.Repository<EntityType>().AddAsync(newEntity);
                        } 
                    }
                }
            }

            return Ok();
        }
    }
}