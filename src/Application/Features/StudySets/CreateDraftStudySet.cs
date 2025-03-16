using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Features.Auth;
using FlashcardXpApi.Domain;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;

namespace FlashcardXpApi.Application.Features.StudySets
{
    public static class CreateDraftStudySet
    {
        public class Command : IRequest<Result<string>>
        {

        };

        public class Handler : IRequestHandler<Command, Result<string>>
        {

            private readonly ICurrentUserService _currentUserService;
            private readonly DataContext _context;

            public Handler(ICurrentUserService currentUserService, DataContext context)
            {
                _currentUserService = currentUserService;
                _context = context;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {

                var user = await _currentUserService.GetCurrentUser();

                if (user is null)
                {
                    return Result.Failure<string>(AuthErrors.AuthenticationRequiredError);
                }

                var newStudySet = new StudySet
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = "",
                    Description = "",
                    Status = StudySetStatus.Draft,
                    CreatedById = user.Id
                };

                var flashcards = new List<Flashcard>
                {
                    new Flashcard { Definition = "", Term = "", StudySetId = newStudySet.Id },
                    new Flashcard { Definition = "", Term = "", StudySetId = newStudySet.Id },
                    new Flashcard { Definition = "", Term = "", StudySetId = newStudySet.Id },
                    new Flashcard { Definition = "", Term = "", StudySetId = newStudySet.Id },
                };

                _context.StudySets.Add(newStudySet);
                await _context.Flashcards.AddRangeAsync(flashcards);

                await _context.SaveChangesAsync(cancellationToken);
                
                return Result.Success(newStudySet.Id);

            }
        }
    }
}
