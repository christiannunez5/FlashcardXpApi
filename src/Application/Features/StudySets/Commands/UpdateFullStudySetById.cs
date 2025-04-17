using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Flashcards.Payloads;
using Domain.Entities.Flashcards;
using Domain.Entities.Studysets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands
{
    public static class UpdateFullStudySetById
    {
        public class Command : IRequest<Result>
        {
            public required string Id { get; set; }
            public required string Title { get; set; }
            public string Description { get; set; } = string.Empty;

            public List<UpdateFlashcardRequest> Flashcards = new();
        }

        public class Handler : IRequestHandler<Command, Result>
        {

            private readonly IApplicationDbContext _context;
            private readonly IUserContext _userContext;
            public Handler(IApplicationDbContext context, IUserContext userContext)
            {
                _context = context;
                _userContext = userContext;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var studySet = await _context
                    .StudySets
                    .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

                if (studySet == null)
                {
                    return Result.Failure(StudySetErrors.StudySetNotFound);
                }

                if (studySet.CreatedById != _userContext.UserId())
                {
                    return Result.Failure(StudySetErrors.NotOwner);
                }

                studySet.Title = request.Title;
                studySet.Description = request.Description;
                studySet.Status = StudySetStatus.Published;
                _context.StudySets.Update(studySet);

                foreach (var flashcard in request.Flashcards)
                {
                    var currentFlashcard = await _context
                        .Flashcards
                        .FirstOrDefaultAsync(s => s.Id == flashcard.Id, cancellationToken);

                    if (currentFlashcard == null)
                    {
                        var newFlashcard = new Flashcard
                        {
                            Term = flashcard.Term,
                            Definition = flashcard.Definition,
                            StudySetId = studySet.Id
                        };
                        _context.Flashcards.Add(newFlashcard);
                    }
                    else
                    {
                        currentFlashcard.Term = flashcard.Term;
                        currentFlashcard.Definition = flashcard.Definition;
                        _context.Flashcards.Update(currentFlashcard);
                    }
                }
                
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
