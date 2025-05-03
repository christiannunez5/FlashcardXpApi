using Application.Common.Abstraction;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Queries;

public static class GetUserStudySetRating
{
    public class Query : IRequest<Result<int>>
    {
        public required string StudySetId { get; init; }
    }
    
    public class Handler : IRequestHandler<Query, Result<int>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;
        
        public Handler(IApplicationDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<Result<int>> Handle(Query request, CancellationToken cancellationToken)
        {
            var studySet = await 
                _context
                .StudySets
                .FirstOrDefaultAsync(s => s.Id == request.StudySetId,
                cancellationToken);
            
            if (studySet == null)
            {
                return Result.Failure<int>(StudySetErrors.StudySetNotFound);
            }
            
            var userReviewed = await _context
                .StudySetRatings
                .FirstOrDefaultAsync(sr => sr.StudySetId == request.StudySetId &&
                        sr.RatedById == _userContext.UserId(), cancellationToken);
            
            if (userReviewed == null)
            {
                return Result.Success(-1);
            }
            
            return Result.Success(userReviewed.Rating);
            
        }
    }
}