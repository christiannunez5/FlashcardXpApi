
using AutoMapper;
using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Contracts.Auth;
using FlashcardXpApi.Application.Contracts.UserExperience;
using FlashcardXpApi.Application.Features.Auth;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.Experience;

public static class GetCurrentUserExperience
{
    public class Query : IRequest<Result<UserExperienceResponse>>
    {
        
    }
    
    public class Handler : IRequestHandler<Query, Result<UserExperienceResponse>>
    {
        
        private readonly ICurrentUserService _currentUserService;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        
        public Handler(ICurrentUserService currentUserService, DataContext context, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Result<UserExperienceResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var currentUser = await _currentUserService.GetCurrentUser();
            
            if (currentUser == null)
            {
                return Result.Failure<UserExperienceResponse>(AuthErrors.AuthenticationRequiredError);
            }
            
            var userXp = await _context
                .UserExperiences
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.UserId == currentUser.Id, cancellationToken);
            
            if (userXp is null)
            {
                return Result.Failure<UserExperienceResponse>(UserExperienceErrors.UserExperienceNotFound);
            }

            var response = new UserExperienceResponse(
                _mapper.Map<UserResponse>(currentUser),
                userXp.Xp,
                new LevelDto((int)userXp.GetLevel, userXp.GetLevel.ToString()),
                userXp.GetMaxXp
            );
            
            return Result.Success(response);
        }
    }
}

