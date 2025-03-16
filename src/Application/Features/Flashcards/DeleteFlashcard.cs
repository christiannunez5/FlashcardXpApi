using AutoMapper;
using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Contracts;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.Flashcards
{
    public static class DeleteFlashcard
    {
        public class Command : IRequest<Result<string>>
        {
            public required string Id { get; set; }
        };

        public class Handler : IRequestHandler<Command, Result<string>>
        {

            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                var flashcard = await _context
                    .Flashcards
                    .FirstOrDefaultAsync(f => f.Id == request.Id);

                if (flashcard is null)
                {
                    return Result.Failure<string>(FlashcardErrors.FlashcardNotFoundError);
                }

                _context.Flashcards.Remove(flashcard);
                await _context.SaveChangesAsync();

                return Result.Success(flashcard.Id);
            }
        }
    }
}
