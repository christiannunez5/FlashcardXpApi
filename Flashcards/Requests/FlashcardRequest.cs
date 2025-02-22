using FluentValidation;

namespace FlashcardXpApi.Flashcards.Requests
{
    public record FlashcardRequest(string Term, string Definition);


    public class FlashcardRequestValidator : AbstractValidator<FlashcardRequest>
    {
        public FlashcardRequestValidator()
        {
            RuleFor(x => x.Term)
                .NotEmpty()
                    .WithMessage("Term can't be empty.");

            RuleFor(x => x.Definition)
                .NotEmpty()
                    .WithMessage("Definition can't be empty.");

        }
    }
}
