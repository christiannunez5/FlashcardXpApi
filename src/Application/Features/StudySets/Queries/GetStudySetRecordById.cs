using Application.Common.Abstraction;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Queries;

public static class GetStudySetRecordById
{
    public class Query : IRequest<Result<int>>
    {
        public required string StudySetId { get; init; }
    }
    
    public class Handler : IRequestHandler<Query, Result<int>>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(Query request, CancellationToken cancellationToken)
        {
            var studyset = await _context
                .StudySets
                .FirstOrDefaultAsync(s => s.Id == request.StudySetId, cancellationToken);

            if (studyset == null)
            {
                return Result.Failure<int>(StudySetErrors.StudySetNotFound);
            }
            
            var studySetRecords = await _context
                .StudySetRecords
                .Where(sr => sr.StudySetId == request.StudySetId)
                .CountAsync(cancellationToken);

            return Result.Success(studySetRecords);
        }
    }
}