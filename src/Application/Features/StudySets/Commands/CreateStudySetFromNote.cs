using Application.Common.Abstraction;
using Application.Common.Models;
using Domain.Entities.Studysets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands;

public static class CreateStudySetFromNote
{
    public class Command : IRequest<Result<string>>
    {
        public string? StudySetId { get; set; }
        public required string NoteContent { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IUserContext _userContext;
        private readonly IApplicationDbContext _context;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IAiService _aiService;
        
        public Handler(IUserContext userContext, IApplicationDbContext context, IDateTimeProvider dateTimeProvider, IAiService aiService)
        {
            _userContext = userContext;
            _context = context;
            _dateTimeProvider = dateTimeProvider;
            _aiService = aiService;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var studySet = await _context
                .StudySets
                .FirstOrDefaultAsync(s => s.Id == request.StudySetId, cancellationToken);   
            
            var newFlashcards = await _aiService
                .GenerateFlashcardFromText(request.NoteContent, cancellationToken);

            if (studySet is null)
            {
                studySet = new StudySet
                {
                    Title = "Untitled",
                    CreatedById = _userContext.UserId(),
                    CreatedAt = DateOnly.FromDateTime(_dateTimeProvider.Today()),
                    Flashcards = newFlashcards
                };
                _context.StudySets.Add(studySet);

            }
            else
            {
                foreach (var flashcard in newFlashcards)
                {
                    studySet.Flashcards.Add(flashcard);
                }
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(studySet.Id);
        }
    }
}