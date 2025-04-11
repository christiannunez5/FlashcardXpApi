

using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Flashcards.Payloads;
using Application.Features.StudySets;
using AutoMapper;
using Domain.Entities.Flashcards;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Flashcards.Commands;

public static class CreateFlashcard
{
    public class Command : IRequest<Result<FlashcardDto>>
    {
        public required string StudySetId { get; set; }
        public required string Term { get; set; }
        public required string Definition { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<FlashcardDto>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<FlashcardDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var studySet = await _context
                .StudySets
                .FirstOrDefaultAsync(s => s.Id == request.StudySetId, cancellationToken);

            if (studySet == null)
            {
                return Result.Failure<FlashcardDto>(StudySetErrors.StudySetNotFound);
            }

            var newFlashcard = new Flashcard
            {
                Term = request.Term,
                Definition = request.Definition,
                StudySetId = request.StudySetId
            };
            _context.Flashcards.Add(newFlashcard);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(_mapper.Map<FlashcardDto>(newFlashcard));
        }
    }
}
