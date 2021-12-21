using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models.DTO.Common;
using Application.Common.Models.Helpers;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Features.Common.EntityTypes
{
    public class GetEntityTypesQuery : ResourceParameters, IRequest<PaginatedList<EntityTypeOutDto>>
    {
        public class GetEntityTypesQueryHandler : IRequestHandler<GetEntityTypesQuery, PaginatedList<EntityTypeOutDto>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public GetEntityTypesQueryHandler(
                IApplicationDbContext context, 
                IMapper mapper)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));
                
                _mapper = mapper ??
                    throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<PaginatedList<EntityTypeOutDto>> Handle(
                GetEntityTypesQuery request, 
                CancellationToken cancellationToken)
            {
                return await _context.EntityTypes
                    .ProjectTo<EntityTypeOutDto>(_mapper.ConfigurationProvider)
                    .PaginatedListAsync(request.PageNumber, request.PageSize);
            }
        }
    }
}