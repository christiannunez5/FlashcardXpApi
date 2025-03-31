using AutoMapper;
using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Domain;
using FlashcardXpApi.Infrastructure.Persistence;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.Auth
{
    public static class CreateUser
    {
        public class Command : IRequest<Result>
        {
            public required string Email { get; set; }
            public required string Username { get; set; }
            public required string Password { get; set; }
            public string? ProfilePicUrl { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email address.");

                RuleFor(x => x.Username).NotEmpty().WithMessage("Username can't be empty.");

            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {

            private readonly UserManager<User> _userManager;
            private readonly IValidator<Command> _validator;
            private readonly DataContext _context;

            public Handler(UserManager<User> userManager, IValidator<Command> validator, DataContext context)
            {
                _validator = validator;
                _userManager = userManager;
                _context = context;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {

                var validationResult = _validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    var errorMessage =
                        validationResult.Errors
                        .Select(x => x.ErrorMessage)
                        .First();

                    return Result.Failure(AuthErrors.ValidationError(errorMessage));
                }

                var newUser = new User
                {
                    Email = request.Email,
                    PasswordHash = request.Password,
                    ProfilePicUrl = request.ProfilePicUrl,
                    UserName = request.Username
                };

                var createdUser = await _userManager.CreateAsync(newUser, request.Password);

                if (!createdUser.Succeeded)
                {
                    var errorMessage = createdUser.Errors.First().Description;
                    return Result.Failure(
                        AuthErrors.ValidationError(errorMessage));
                }

                var quests = await _context.Quests.ToListAsync();

                var userQuests = quests.Select(q =>
                {
                    return new UserQuest
                    {
                        UserId = newUser.Id,
                        QuestId = q.Id
                    };
                }).ToList();

                await _context.UserQuests.AddRangeAsync(userQuests);
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();

            }
        }

    }

}
