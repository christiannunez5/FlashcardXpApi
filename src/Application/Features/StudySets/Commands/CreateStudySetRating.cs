using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.StudySets.Payloads;
using AutoMapper;
using Domain.Entities.Studysets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands;

public static class CreateStudySetRating
{
    public class Command : IRequest<Result<StudySetRatingDto>>
    {
        public required string StudySetId {get; init;}
        public int Rating { get; init; }
        public string ReviewText { get; init; } = string.Empty;
    }
    
    public class Handler : IRequestHandler<Command, Result<StudySetRatingDto>>
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
            
           var newStudySetRating = new StudySetRating
           {
               RatedById = _userContext.UserId(),
               StudySetId = studySet.Id,
               Rating = request.Rating,
               ReviewText = request.ReviewText
           };
           
           _context.StudySetRatings.Add(newStudySetRating);
           await _context.SaveChangesAsync(cancellationToken);
            
           return Result.Success(_mapper.Map<StudySetRatingDto>(studySet));
        }
    }
}