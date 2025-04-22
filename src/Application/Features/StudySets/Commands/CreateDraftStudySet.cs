
using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Flashcards.Payloads;
using Domain.Entities.Flashcards;
using Domain.Entities.Studysets;
using MediatR;

namespace Application.Features.StudySets.Commands
{
    public static class CreateDraftStudySet
    {
        public class Command : IRequest<Result<string>>
        {

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

                var newStudySet = new StudySet
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "",
                    Description = "",
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

                await _context.Flashcards.AddRangeAsync(flashcards);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success(newStudySet.Id);
            }
        }
    }
}
