using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.StudySets.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Queries;

public static class GetCurrentUserRecentStudySets
{
    public class Query : IRequest<Result<List<RecentStudySetDto>>>
    {

    };
    
    public class Handler :  IRequestHandler<Query, Result<List<RecentStudySetDto>>>
    {
        
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;

        public Handler(IMapper mapper, IApplicationDbContext context, IUserContext userContext)
        {
            _mapper = mapper;
            _context = context;
            _userContext = userContext;
        }

        public async Task<Result<List<RecentStudySetDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var recentStudySets = await _context
                .RecentStudySets
                .Where(rs => rs.UserId == _userContext.UserId())
                .ToListAsync(cancellationToken);
            
            return Result.Success(_mapper.Map<List<RecentStudySetDto>>(recentStudySets));
        }
    }
}