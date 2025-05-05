
using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Flashcards.Payloads;
using Application.Features.Folders;
using Domain.Entities.Flashcards;
using Domain.Entities.Studysets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands
{
    public static class CreateDraftStudySet
    {
        public class Command : IRequest<Result<string>>
        {
            public string? FolderId { get; set; }
        };

        public class Handler : IRequestHandler<Command, Result<string>>
        {

            private readonly IApplicationDbContext _context;
            private readonly IUserContext _userContext;
            private readonly IDateTimeProvider _dateTimeProvider;
            public Handler(IApplicationDbContext context, IUserContext userContext, IDateTimeProvider dateTimeProvider)
            {
                _context = context;
                _userContext = userContext;
                _dateTimeProvider = dateTimeProvider;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                
                if (request.FolderId != null)
                {
                    var doesFolderExist = await _context
                        .Folders
                        .AnyAsync(f => f.Id == request.FolderId, cancellationToken);

                    if (!doesFolderExist)
                    {
                        return Result.Failure<string>(FolderErrors.FolderNotFound);
                    }
                }
                
                var newStudySet = new StudySet
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "Untitled",
                    Description = "",
                    FolderId = request.FolderId,
                    Status = StudySetStatus.Draft,
                    CreatedById = _userContext.UserId(),
                    CreatedAt = DateOnly.FromDateTime(_dateTimeProvider.Today())
                };

                _context.StudySets.Add(newStudySet);

                var flashcards = new List<Flashcard>
                {
                    new Flashcard { Definition = "", Term = "", StudySetId = newStudySet.Id },
                    new Flashcard { Definition = "", Term = "", StudySetId = newStudySet.Id },
                    new Flashcard { Definition = "", Term = "", StudySetId = newStudySet.Id },
                    new Flashcard { Definition = "", Term = "", StudySetId = newStudySet.Id },
                };

                await _context.Flashcards.AddRangeAsync(flashcards, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success(newStudySet.Id);
            }
        }
    }
}
