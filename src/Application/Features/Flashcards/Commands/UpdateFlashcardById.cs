using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Flashcards.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Flashcards.Commands;

public static class UpdateFlashcardById
{
    public class Command : IRequest<Result<FlashcardDto>>
    {
        public required string Id { get; init; }
        public required string Term { get; init; }
        public required string Definition { get; init; }
        
    };
    
    public class Handler : IRequestHandler<Command, Result<FlashcardDto>>
    {
        
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public Handler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<FlashcardDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var flashcard = await _context
                .Flashcards
                .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

            if (flashcard == null)
            {
                return Result.Failure<FlashcardDto>(FlashcardErrors.FlashcardNotFound);
            }

            flashcard.Term = request.Term;
            flashcard.Definition = request.Definition;
            
            _context.Flashcards.Update(flashcard);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success(_mapper.Map<FlashcardDto>(flashcard));
        }
    }
}