using FluentValidation;

namespace FlashcardXpApi.Features.Auth
{
    public record CreateUserRequest(
        string Email,
        string Username,
        string Password,
        string? ProfilePicUrl);

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                    .WithMessage("Invalid email address.");

            RuleFor(x => x.Username)
               .NotEmpty()
                    .WithMessage("Username can't be empty.");

        }
    }
}
