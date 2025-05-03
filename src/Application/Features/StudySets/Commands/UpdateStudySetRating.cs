using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.StudySets.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands;

public static class UpdateStudySetRating
{
    public class Command : IRequest<Result<StudySetRatingDto>>
    {
        public required string StudySetId { get; init; }
        public double Rating { get; init; }
        public string ReviewText { get; init; } = string.Empty;
    }
    
    public class Handler : IRequestHandler<Command, Result<StudySetRatingDto>>
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

        public async Task<Result<StudySetRatingDto>> Handle(Command request, CancellationToken cancellationToken)
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
            
            var studySetRating = await _context
                .StudySetRatings
                .FirstOrDefaultAsync(sr => sr.StudySetId == request.StudySetId &&
                                           sr.RatedById == _userContext.UserId(), cancellationToken);
            
            if (studySetRating == null)
            {
                // TODO : add an error dedicated to this
                return Result.Failure<StudySetRatingDto>(StudySetErrors.StudySetNotFound);
            }
            
            studySetRating.Rating = (int) request.Rating;
            studySetRating.ReviewText = request.ReviewText;

            _context.StudySetRatings.Update(studySetRating);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(_mapper.Map<StudySetRatingDto>(studySet));

        }
    }
}