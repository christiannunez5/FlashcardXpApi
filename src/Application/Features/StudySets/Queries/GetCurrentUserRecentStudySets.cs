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

        public Handler(IApplicationDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<Result<List<RecentStudySetDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var recentStudySets = await _context
                .RecentStudySets
                .Include(rs => rs.StudySet)
                .Where(rs => rs.UserId == _userContext.UserId())
                .Select(rs => new RecentStudySetDto
                (
                    rs.Id,
                    rs.StudySet.Title,
                    rs.AccessedAt
                ))
                .ToListAsync(cancellationToken);
            
            return Result.Success(recentStudySets);

        }
    }
}