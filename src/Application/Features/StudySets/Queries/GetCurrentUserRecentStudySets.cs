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
        
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext context, IUserContext userContext, IMapper mapper)
        {
            _context = context;
            _userContext = userContext;
            _mapper = mapper;
        }
        
        public async Task<Result<List<RecentStudySetDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var recentStudySets = await _context
                .RecentStudySets
                .Include(rs => rs.StudySet)
                .ThenInclude(s => s.CreatedBy)
                .OrderByDescending(rs => rs.AccessedAt)
                .Where(rs => rs.UserId == _userContext.UserId())
                .ToListAsync(cancellationToken);
            
            return Result.Success(_mapper.Map<List<RecentStudySetDto>>(recentStudySets));

        }
    }
}