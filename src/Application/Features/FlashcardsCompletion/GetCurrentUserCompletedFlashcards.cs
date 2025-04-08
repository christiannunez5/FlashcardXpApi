using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Features.Auth;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.FlashcardsCompletion
{
    public static class GetCurrentUserCompletedFlashcards
    {
        public class Query : IRequest<Result<int>>
        {

        };

        public class Handler : IRequestHandler<Query, Result<int>>
        {

            private readonly DataContext _context;
            private readonly ICurrentUserService _currentUserService;

            public Handler(DataContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<Result<int>> Handle(Query request, CancellationToken cancellationToken)
            {

                var user = await _currentUserService.GetCurrentUser();

                if (user is null)
                {
                    return Result.Failure<int>(AuthErrors.AuthenticationRequiredError);
                }
                
                var flashcardsCountCompletedToday = await _context
                    .FlashcardsCompleted
                    .Where(fc => fc.UserId == user.Id && fc.Date == DateOnly.MinValue)
                    .CountAsync(cancellationToken);
                
                return Result.Success(flashcardsCountCompletedToday);
            }
        }
    }
}
