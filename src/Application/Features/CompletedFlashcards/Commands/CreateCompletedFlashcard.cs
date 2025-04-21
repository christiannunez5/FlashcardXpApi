using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Flashcards;
using Domain.Entities.Flashcards;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CompletedFlashcards.Commands;

public static class CreateCompletedFlashcard
{
    public class Command : IRequest<Result>
    {
        public required string FlashcardId { get; set; }
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
            var doesFlashcardExist = await _context
                .Flashcards
                .AnyAsync(f => f.Id == request.FlashcardId, cancellationToken);
            
            if (!doesFlashcardExist)
            {
                return Result.Failure(FlashcardErrors.FlashcardNotFound);
            }

            var isFlashcardMarkedAsCompletedToday = await _context
                .CompletedFlashcards
                .AnyAsync(cf => cf.UserId == _userContext.UserId()
                            && cf.FlashcardId == request.FlashcardId, cancellationToken);

            if (isFlashcardMarkedAsCompletedToday)
            {
                return Result.Failure(CompletedFlashcardsErrors.CannotMarkCompleteAgain);
            }

            var newCompletedFlashcard = new CompletedFlashcard
            {
                FlashcardId = request.FlashcardId,
                UserId = _userContext.UserId(),
            };
            
            _context.CompletedFlashcards.Add(newCompletedFlashcard);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();    
        }
    }
}