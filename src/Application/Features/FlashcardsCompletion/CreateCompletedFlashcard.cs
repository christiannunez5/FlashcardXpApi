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
        public class Command : IRequest<Result<string>>
        {
            public required string FlashcardId { get; set; }
        };
        
        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly DataContext _context;
            private readonly ICurrentUserService _currentUserService;

            public Handler(DataContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {

                var user = await _currentUserService.GetCurrentUser();

                if (user is null)
                {
                    return Result.Failure<string>(AuthErrors.AuthenticationRequiredError);
                }
                
                var isFlashcardCompleted = await _context
                    .FlashcardsCompleted
                    .AnyAsync(fc => fc.UserId == user.Id && fc.FlashcardId == request.FlashcardId, cancellationToken);
                
                
                if (isFlashcardCompleted) 
                {
                    return Result.Failure<string>(CompletedFlashcardsErrors.CannotMarkCompletedAgain);
                }
                
                var flashcardCompleted = new FlashcardsCompleted
                {
                    FlashcardId = request.FlashcardId,
                    UserId = user.Id,
                };

                _context.FlashcardsCompleted.Add(flashcardCompleted);
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success(flashcardCompleted.FlashcardId);
                
            }
        }
    }
}
