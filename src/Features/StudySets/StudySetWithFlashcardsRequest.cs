using FlashcardXpApi.Features.Flashcards;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FlashcardXpApi.Features.StudySets
{
    public record StudySetWithFlashcardsRequest(
        string Title,
        string Description,
        bool IsPublic,
        List<FlashcardRequest> Flashcards
    );

    public class StudySetRequestValidator : AbstractValidator<StudySetWithFlashcardsRequest>
    {
        public StudySetRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage("Title can't be empty.");

            RuleFor(x => x.Flashcards)
                .Must(flashcards => flashcards.Count >= 4)
                    .WithMessage("Please create atleast 4 flashcards.")

                .ForEach(flashcard => flashcard.SetValidator(new FlashcardRequestValidator()));
        }
    }


}
