using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Features.Auth;
using FlashcardXpApi.Domain;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.Experience;

public static class IncrementUserExperience
{
    public class Command : IRequest<Result<int>>
    {
        public int Xp { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<int>>
    {

        private readonly ICurrentUserService _currentUserService;
        private readonly DataContext _context;

        public Handler(ICurrentUserService currentUserService, DataContext context)
        {
            _currentUserService = currentUserService;
            _context = context;
        }
    
        public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentUserService.GetCurrentUser();

            if (currentUser == null)
            {
                return Result.Failure<int>(AuthErrors.AuthenticationRequiredError);
            }
            
            var userExperience = await _context
                .UserExperiences
                .FirstOrDefaultAsync(ux => ux.UserId == currentUser.Id, cancellationToken);

            if (userExperience == null)
            {
                return Result.Failure<int>(UserExperienceErrors.UserExperienceNotFound);
            }
            
            userExperience.Xp += request.Xp;

            _context.UserExperiences.Update(userExperience);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(userExperience.Xp);

        }
    }
}