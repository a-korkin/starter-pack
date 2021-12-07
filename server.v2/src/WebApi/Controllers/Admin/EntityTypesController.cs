using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

            Type tt = _context.GetType();
            var d = tt.GetFields();

            foreach (var a in d)
            {
                Console.WriteLine(a);
            }

            return Ok();
        }
    }
}