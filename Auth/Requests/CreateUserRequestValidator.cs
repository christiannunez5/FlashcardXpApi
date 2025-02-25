using FluentValidation;

namespace FlashcardXpApi.Auth.Requests
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

        }
    }
}
