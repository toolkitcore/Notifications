using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notifications.Application.Common.Exceptions;
using Notifications.Application.Common.Interfaces;
using Notifications.Application.Common.Models.Apps;
using Notifications.Domain.Entities;

namespace Notifications.Application.Apps.Queries.GetApp;

public record GetAppQuery(Guid Id) : IRequest<AppDto>;

public class GetAppQueryHandler : IRequestHandler<GetAppQuery, AppDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAppQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<AppDto> Handle(GetAppQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.Apps
            .Where(x => x.Id == request.Id)
            .ProjectTo<AppDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        
        if(entities.FirstOrDefault() == null)
            throw new NotFoundException(nameof(App), request.Id);

        return entities.FirstOrDefault();

    }
}