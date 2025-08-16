using Application.Common.Abstraction;
using Application.Common.Models;
using Domain.Entities.Auth;
using Domain.Entities.Quests;
using Domain.Entities.UserExperiences;
using Domain.Entities.Users;
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
        private readonly IDateTimeProvider _dateTimeProvider;
        
        public Handler(UserManager<User> userManager, IApplicationDbContext context, 
            IDateTimeProvider dateTimeProvider)
        {
            _userManager = userManager;
            _context = context;
            _dateTimeProvider = dateTimeProvider;
        }
        
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            
            string defaultProfilePicUrl = $"https://api.dicebear.com/7.x/micah/svg?seed={request.Username}";
            
            // TODO: add multer to store profile picture
            var newUser = new User()
            {
                Email = request.Email,
                UserName = request.Username,
                PasswordHash = request.Password,
                ProfilePicUrl = request.ProfilePic == "" ? defaultProfilePicUrl : request.ProfilePic
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
                    QuestId = q.Id,
                    CurrentQuestDate = DateOnly.FromDateTime(_dateTimeProvider.Today()),
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