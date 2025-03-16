using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Features.Auth;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.StudySets
{
    public static class DeleteStudySet
    {
        public class Command : IRequest<Result<string>>
        {
            public required string Id { get; set; }
        }

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

                var studySet = await _context
                    .StudySets
                    .FirstOrDefaultAsync(s => s.Id == request.Id);

                if (studySet is null)
                {
                    return Result.Failure<string>(StudySetErrors.StudySetNotFoundError);
                }

                if (studySet.CreatedById != user.Id )
                {
                    return Result.Failure<string>(AuthErrors.AuthorizationFailedError);
                }

                _context.StudySets.Remove(studySet);
                await _context.SaveChangesAsync();

                return Result.Success(studySet.Id);
            }
        }
    }
}
