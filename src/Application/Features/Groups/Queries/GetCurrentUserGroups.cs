using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Groups.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Groups.Queries;

public static class GetCurrentUserGroups
{
    public class Query : IRequest<Result<List<GroupBriefDto>>>
    {
        
    }
    
    public class Handler : IRequestHandler<Query, Result<List<GroupBriefDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext context, IUserContext userContext, IMapper mapper)
        {
            _context = context;
            _userContext = userContext;
            _mapper = mapper;
        }

        public async Task<Result<List<GroupBriefDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            
            var userGroups = await _context
                .Groups
                .Include(g => g.GroupMembers)
                .ThenInclude(gm => gm.User)
                .Include(g => g.CreatedBy)
                .Where(
                    g => g.CreatedById == _userContext.UserId() ||
                         g.GroupMembers.Any(gm => gm.UserId == _userContext.UserId())
                )
                .ToListAsync(cancellationToken);

            return Result.Success(_mapper.Map<List<GroupBriefDto>>(userGroups));

        }
    }
}