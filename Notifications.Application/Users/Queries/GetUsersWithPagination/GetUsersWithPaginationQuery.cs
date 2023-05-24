using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Common.Mappings;
using Notifications.Application.Common.Models.PaginatedList;
using Notifications.Application.Common.Models.Users;

namespace Notifications.Application.Users.Queries.GetUsersWithPagination;

// triển khai giao diện IRequest của gói MediatR
// IRequest là một interfaces chung
// PaginatedList<UserDto> là kết quả mà tôi mong muốn
public record GetUsersWithPaginationQuery : IRequest<PaginatedList<UserDto>>
{
    public int PageIndex { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string TextSearch { get; init; } = string.Empty;
}

// GetUsersWithPaginationQueryHandler implements the IRequestHandler interface of MediatR
// IRequestHandler là một giao diện chung
// IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<UserDto>>? Điều này có nghĩa là nếu yêu cầu thuộc loại GetUsersWithPaginationQuery thì trả về  PaginatedList<UserDto>.

public class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<UserDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUsersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PaginatedList<UserDto>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Where(u => string.IsNullOrEmpty(request.TextSearch) ||
                        (!string.IsNullOrEmpty(request.TextSearch) ||
                         u.Code.Contains(request.TextSearch) ||
                         u.UserName.Contains(request.TextSearch) ||
                         (string.IsNullOrEmpty(u.FullName) || u.FullName.Contains(request.TextSearch))))
            .OrderBy(u => u.UserName)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageIndex, request.PageSize)
            .ConfigureAwait(false);
    }
}