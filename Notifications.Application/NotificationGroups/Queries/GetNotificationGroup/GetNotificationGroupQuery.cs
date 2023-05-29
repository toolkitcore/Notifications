using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Common.Models.PaginatedList;
using Notifications.Application.NotificationGroups.Models;
using Notifications.Domain.Entities;

namespace Notifications.Application.NotificationGroups.Queries.GetNotificationGroup;

public record GetNotificationGroupQuery(Guid groupId) : IRequest<NotificationGroupDto>;

public class GetNotificationGroupQueryHandler : IRequestHandler<GetNotificationGroupQuery, NotificationGroupDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetNotificationGroupQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<NotificationGroupDto> Handle(GetNotificationGroupQuery request, CancellationToken cancellationToken)
    {
        var notificationGroup =
            await _context.NotificationGroups.Where(n => n.Id == request.groupId)
                .Include(n => n.App)
                .ProjectTo<NotificationGroupDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

        if (notificationGroup is null)
            throw new NotFoundException(nameof(notificationGroup), request.groupId);

        return notificationGroup;
    }
}