using AutoMapper;
using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Contracts.UserExperience;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.Leaderboard;

public static class GetLeaderboardByExperience
{
    public class Query : IRequest<Result<List<UserExperienceResponse>>>
    {
        
    };
    
    public class Handler : IRequestHandler<Query, Result<List<UserExperienceResponse>>>
    {
        
        private readonly DataContext  _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Result<List<UserExperienceResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var topUserExperience = await _context
                .UserExperiences
                .Include(xp => xp.User)
                .OrderByDescending(uxp => uxp.Xp)
                .ToListAsync(cancellationToken);
            
            return Result.Success(_mapper.Map<List<UserExperienceResponse>>(topUserExperience));

        }
    }
}