using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Common.Mappings;
using Notifications.Application.Common.Models.Abstractions;
using Notifications.Application.Common.Models.PaginatedList;
using Notifications.Application.NotificationGroups.Models;

namespace Notifications.Application.NotificationGroups.Queries.GetNotificationGroupsWithPaginationQuery;

public class GetNotificationGroupsWithPaginationQuery : PaginationQuery ,IRequest<PaginatedList<NotificationGroupDto>>
{
    public Guid? NotificationGroupId { get; set; }
}

public class GetNotificationGroupsWithPaginationQueryHandler : IRequestHandler<GetNotificationGroupsWithPaginationQuery,
    PaginatedList<NotificationGroupDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetNotificationGroupsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<PaginatedList<NotificationGroupDto>> Handle(GetNotificationGroupsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.NotificationGroups
            .Where(x => 
                (!request.NotificationGroupId.HasValue || x.Id == request.NotificationGroupId) ||
                (string.IsNullOrEmpty(request.TextSearch) || (x.Name.Contains(request.TextSearch) || x.Name.Contains(request.TextSearch))))
            .OrderBy(x => x.Name)
            .ProjectTo<NotificationGroupDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageIndex, request.PageSize);
    }
}