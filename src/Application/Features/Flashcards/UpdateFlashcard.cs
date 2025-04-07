using AutoMapper;
using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Contracts;
using FlashcardXpApi.Application.Contracts.Flashcards;
using FlashcardXpApi.Domain;
using FlashcardXpApi.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.Flashcards
{
    public static class UpdateFlashcard
    {
        public class Command : IRequest<Result<FlashcardResponse>>
        {
            public required string Id { get; set; }
            public required string StudySetId { get; set; }
            public required string Term { get; set; }
            public string Definition { get; set; } = string.Empty;
        };

        public class Validator : AbstractValidator<Command>
        {
            public Validator() {
                RuleFor(x => x.Term)
                    .NotEmpty().WithMessage("Term can't be empty.");
                
                RuleFor(x => x.Definition)
                    .MinimumLength(6).WithMessage("Description can't be empty.");

            }
        }

        public class Handler : IRequestHandler<Command, Result<FlashcardResponse>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IValidator<Command> _validator;

            public Handler(DataContext context, IMapper mapper, IValidator<Command> validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<Result<FlashcardResponse>> Handle(Command request, CancellationToken cancellationToken)
            {

                var validationResult = _validator.Validate(request);
                
                if (!validationResult.IsValid)
                {
                    var error = validationResult
                        .Errors
                        .Select(x => x.ErrorMessage)
                        .First();

                    return Result.Failure<FlashcardResponse>(FlashcardErrors.ValidationError(error));
                };

                var flashcard = await _context
                    .Flashcards
                    .FirstOrDefaultAsync(f => f.Id == request.Id);
                    
                if (flashcard is null)
                {
                    var newFlashcard = new Flashcard()
                    {
                         Term = request.Term,
                         Definition = request.Definition,
                         StudySetId = request.StudySetId
                    };
                    _context.Flashcards.Add(newFlashcard);
                }

                else
                {
                    flashcard.Term = request.Term;
                    flashcard.Definition =
                        request.Definition == "" ? request.Definition : flashcard.Definition;
                    _context.Flashcards.Update(flashcard);
                }
                
                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success(_mapper.Map<FlashcardResponse>(flashcard));
                
            }
        }
    }
}
