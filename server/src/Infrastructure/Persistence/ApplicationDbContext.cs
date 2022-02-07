using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Attributes;
using Domain.Entities.Admin;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt) {}

        public DbSet<EntityType> EntityTypes { get; set; }

        public DbSet<Entity> Entities { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Task.Run(() => CreatePartitionTables());
            base.OnModelCreating(builder);
        }

        private async Task CreatePartitionTables()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var types = assemblies
                .SelectMany(s => s.GetTypes().Where(w => w.FullName.Contains("Entities")));

            var existingEntityTypes = await this.EntityTypes.ToListAsync();

            foreach (var type in types)
            {
                DescriptionAttribute attribute = 
                    (DescriptionAttribute)Attribute.GetCustomAttribute(type, typeof(DescriptionAttribute));

                if (attribute != null)
                {
                    bool entityExists = existingEntityTypes
                        .Any(a => a.Schema == attribute.Schema && a.TableName == attribute.Name);
                    
                    if (!entityExists)
                    {
                        var newEntity = new EntityType
                        {
                            Name = attribute.RuName,
                            Slug = attribute.Slug,
                            Schema = attribute.Schema,
                            TableName = attribute.Name
                        };

                        await this.EntityTypes.AddAsync(newEntity);

                        if (attribute.IsEntityPartition)
                        {
                            // string query = $"CREATE TABLE common.cd_entities_{newEntity.Schema}_{newEntity.Slug} PARTITION OF common.cd_entities FOR VALUES IN ('{newEntity.Id}')";
                            await Database.ExecuteSqlInterpolatedAsync($"CREATE TABLE common.cd_entities_{newEntity.Schema}_{newEntity.Slug} PARTITION OF common.cd_entities FOR VALUES IN ('{newEntity.Id}')");
                        }
                    }
                }
            }
        }
    }
}