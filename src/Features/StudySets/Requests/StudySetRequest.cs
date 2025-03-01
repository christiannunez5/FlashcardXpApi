using FluentValidation;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FlashcardXpApi.Features.StudySets.Requests
{
    public record StudySetRequest(
        string Title,
        string Description,
        bool IsPublic
    );

    public class StudySetRequestValidator : AbstractValidator<StudySetRequest>
    {
        public StudySetRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                    .WithMessage("Title can't be empty.");
        }
    }


}
