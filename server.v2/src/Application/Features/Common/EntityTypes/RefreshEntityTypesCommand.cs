using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models.DTO.Common;
using AutoMapper;
using Domain.Attributes;
using Domain.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Common.EntityTypes
{
    public class RefreshEntityTypesCommand : IRequest<IEnumerable<EntityTypeOutDto>>
    {
        public class RefreshEntityTypesCommandHandler : IRequestHandler<RefreshEntityTypesCommand, IEnumerable<EntityTypeOutDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public RefreshEntityTypesCommandHandler(
                IApplicationDbContext context,
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));

                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(context));
            }

            public async Task<IEnumerable<EntityTypeOutDto>> Handle(RefreshEntityTypesCommand request, CancellationToken cancellationToken)
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

                            string query = $"CREATE TABLE common.cd_entities_{newEntity.Slug} PARTITION OF common.cd_entities FOR VALUES IN ('{newEntity.Id}')";
                            await _context.ExecuteSqlCommandAsync(query);
                        } 
                    }
                }

                existingEntityTypes = await _context.EntityTypes.ToListAsync();

                return _mapper.Map<IEnumerable<EntityTypeOutDto>>(existingEntityTypes);
            }
        }
    }
}