using Application.Common.Abstraction;
using Application.Common.Models;
using Domain.Entities.Auth;
using Domain.Entities.Quests;
using Domain.Entities.UserExperiences;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands;

public static class CreateUser
{
    public class Command : IRequest<Result>
    {
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public string ProfilePic { get; set; } = string.Empty;
    };

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
        private readonly IApplicationDbContext _context;

        public Handler(UserManager<User> userManager, IApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var newUser = new User()
            {
                Email = request.Email,
                UserName = request.Username,
                PasswordHash = request.Password,
                ProfilePicUrl = request.ProfilePic
            };
            
            var createdUser = await _userManager.CreateAsync(newUser, request.Password);
            if (!createdUser.Succeeded)
            {
                var errorMessage = createdUser.Errors.First().Description;
                return Result.Failure(
                    AuthErrors.ValidationError(errorMessage));
            }

            var quests = await _context.Quests.ToListAsync(cancellationToken);

            var userQuests = quests.Select(q =>
            {
                return new UserQuest
                {
                    UserId = newUser.Id,
                    QuestId = q.Id
                };
            }).ToList();
            await _context.UserQuests.AddRangeAsync(userQuests);

            var newUserExperience = new UserExperience
            { 
                UserId = newUser.Id 
            };
            _context.UserExperiences.Add(newUserExperience);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}