using Application.Common.Abstraction;
using Application.Common.Models;
using Domain.Entities.Flashcards;
using Domain.Entities.Studysets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands;

public static class CombineStudySets
{
    public class Command : IRequest<Result<string>>
    {
        public List<string> StudySetIds { get; init; }
    }
    
    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IUserContext _userContext;
        private readonly IApplicationDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;
        
        public Handler(IUserContext userContext, IApplicationDbContext context, 
            IDateTimeProvider dateTimeProvider)
        {
            _userContext = userContext;
            _context = context;
            _dateTimeProvider = dateTimeProvider;
        }
        
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            List<Flashcard> newFlashcards = [];
            
            foreach (var studySetId in request.StudySetIds)
            {
                var studySet = await _context
                    .StudySets
                    .Include(s => s.Flashcards)
                    .FirstOrDefaultAsync(s => s.Id == studySetId,  cancellationToken);

                if (studySet == null)
                {
                    return Result.Failure<string>(StudySetErrors.StudySetNotFound);
                }

                newFlashcards.AddRange(studySet.Flashcards.Select(flashchard => 
                    new Flashcard { Term = flashchard.Term, 
                        Definition = flashchard.Definition,
                        CreatedAt = _dateTimeProvider.Today()
                    }));
            }
            
            var newStudySet = new StudySet
            {
                Title = "Untitled",
                CreatedById = _userContext.UserId(),
                Flashcards = newFlashcards,
                CreatedAt = DateOnly.FromDateTime(_dateTimeProvider.Today())
            };
            
            _context.StudySets.Add(newStudySet);

            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success(newStudySet.Id);
        }
    }
}