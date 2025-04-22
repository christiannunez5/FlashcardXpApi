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
        private readonly IDateTimeProvider _dateTimeProvider;
        
        public Handler(IApplicationDbContext context, IUserContext userContext, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _userContext = userContext;
            _dateTimeProvider = dateTimeProvider;
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
            
            var completedFlashcard = await _context
                .CompletedFlashcards
                .FirstOrDefaultAsync(cf => cf.UserId == _userContext.UserId()
                && cf.FlashcardId == request.FlashcardId, cancellationToken);
            
            if (completedFlashcard == null)
            {
                var newCompletedFlashcard = new CompletedFlashcard
                {
                    FlashcardId = request.FlashcardId,
                    UserId = _userContext.UserId(),
                    Date = DateOnly.FromDateTime(_dateTimeProvider.Today())
                };
                _context.CompletedFlashcards.Add(newCompletedFlashcard);
            }
            else
            {
                if (completedFlashcard.Date == DateOnly.FromDateTime(_dateTimeProvider.Today()))
                {
                    return Result.Failure(CompletedFlashcardsErrors.CannotMarkCompleteAgain);
                }
                completedFlashcard.Date = DateOnly.FromDateTime(_dateTimeProvider.Today());
                _context.CompletedFlashcards.Update(completedFlashcard);
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();    
        }
    }
}