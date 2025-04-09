using Application.Common.Abstraction;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Flashcards.Commands;

public static class DeleteFlashcardById
{
    public class Command : IRequest<Result<string>>
    {
        public required string Id { get; init; }
    }
    
    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            
            var flashcard =  await _context
                .Flashcards
                .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);
            
            if (flashcard == null)
            {
                return Result.Failure<string>(FlashcardErrors.FlashcardNotFound);
            }
            
            _context.Flashcards.Remove(flashcard);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(request.Id);
        }
    }
}