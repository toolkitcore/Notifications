using MediatR;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Domain.Entities;

namespace Notifications.Application.Apps.Commands.DeleteApp;

public record DeleteAppCommand(Guid Id) : IRequest;

public class DeleteAppCommandHandler : IRequestHandler<DeleteAppCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteAppCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteAppCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Apps
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(App), request.Id);

        _context.Apps.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}