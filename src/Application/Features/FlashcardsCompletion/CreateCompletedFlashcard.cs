using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Features.Auth;
using FlashcardXpApi.Domain;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.FlashcardsCompletion
{
    public static class CreateCompletedFlashcard
    {
        public class Command : IRequest<Result>
        {
            public string FlashcardId { get; set; } = string.Empty;
        };

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly DataContext _context;
            private readonly ICurrentUserService _currentUserService;

            public Handler(DataContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {

                var user = await _currentUserService.GetCurrentUser();

                if (user is null)
                {
                    return Result.Failure(AuthErrors.AuthenticationRequiredError);
                }

                var isFlashcardCompleted = await _context
                    .FlashcardsCompleted
                    .AnyAsync(fc => fc.UserId == user.Id && fc.FlashcardId == request.FlashcardId);
                
                if (isFlashcardCompleted) 
                {
                    return Result.Success();
                }

                var flashcardCompleted = new FlashcardsCompleted
                {
                    FlashcardId = request.FlashcardId,
                    UserId = user.Id,
                };

                _context.FlashcardsCompleted.Add(flashcardCompleted);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();

            }
        }
    }
}
