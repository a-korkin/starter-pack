using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Admin.Roles
{
    public class DeleteRoleCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, bool>
        {
            private readonly IApplicationDbContext _context;

            public DeleteRoleCommandHandler(IApplicationDbContext context)
            {
                _context = context ??
                    throw new ArgumentNullException(nameof(context));                
            }

            public async Task<bool> Handle(
                DeleteRoleCommand request, 
                CancellationToken cancellationToken)
            {
                var roleEntity = await _context.Roles.SingleOrDefaultAsync(w => w.Id == request.Id);

                if (roleEntity == null)
                    return false;

                var result = _context.Roles.Remove(roleEntity);
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}