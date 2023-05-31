using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Common.Mappings;
using Notifications.Application.Common.Models.Abstractions;
using Notifications.Application.Common.Models.PaginatedList;
using Notifications.Application.NotificationGroups.Models;
using Shared.Caching.Abstractions;

namespace Notifications.Application.NotificationGroups.Queries.GetsWithPaginationQuery;

public class GetNotificationGroupsWithFilterRequestModel : FilterRequestModel ,IRequest<PaginatedList<NotificationGroupDto>>
{
    public Guid? NotificationGroupId { get; set; }
}

public class GetNotificationGroupsWithPaginationQueryHandler : IRequestHandler<GetNotificationGroupsWithFilterRequestModel,
    PaginatedList<NotificationGroupDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;

    public GetNotificationGroupsWithPaginationQueryHandler(IApplicationDbContext context, ICacheService cacheService, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<PaginatedList<NotificationGroupDto>> Handle(GetNotificationGroupsWithFilterRequestModel request, CancellationToken cancellationToken)
    {
        // var cacheData = _cacheService.GetData<PaginatedList<NotificationGroupDto>> ("notification-groups");
        // if (cacheData is not null)
        //     return cacheData;
        
        var notificationGroupPaginatedList = await _context.NotificationGroups
            .Where(x => 
                (!request.NotificationGroupId.HasValue || x.Id == request.NotificationGroupId) ||
                (string.IsNullOrEmpty(request.TextSearch) || (x.Name.Contains(request.TextSearch) || x.Name.Contains(request.TextSearch))))
            .OrderBy(x => x.Name)
            .ProjectTo<NotificationGroupDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageIndex, request.PageSize);

        var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
        // _cacheService.SetData<PaginatedList<NotificationGroupDto>> ("notification-groups", notificationGroupPaginatedList, expirationTime);
        
        return notificationGroupPaginatedList;
    }
}