using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.StudySets.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Queries;

public static class GetStudySetRatingById
{
    // TODO: Fix Error Mapping
    public class Query : IRequest<Result<StudySetRatingDto>>
    {
        public required string StudySetId { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Result<StudySetRatingDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public Handler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<StudySetRatingDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var studySet = await _context
                .StudySets
                .Include(s => s.StudySetRatings)
                .ThenInclude(s => s.RatedBy)
                .FirstOrDefaultAsync(s => s.Id == request.StudySetId, cancellationToken);
            
            if (studySet == null)
            {
                return Result.Failure<StudySetRatingDto>(StudySetErrors.StudySetNotFound);
            }
        
            return Result.Success(_mapper.Map<StudySetRatingDto>(studySet));

        }
    }
}