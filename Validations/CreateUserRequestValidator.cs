using FlashcardXpApi.Auth.Requests;
using FluentValidation;

namespace FlashcardXpApi.Validations
{
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

            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage("Password can't be null.");
                

        }
    }
}
