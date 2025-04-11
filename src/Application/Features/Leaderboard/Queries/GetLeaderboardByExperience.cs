

using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.UserExperiences.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Leaderboard.Queries;

public static class GetLeaderboardByExperience
{

    public class Query : IRequest<Result<List<UserExperienceDto>>>
    {

    }

    public class Handler : IRequestHandler<Query, Result<List<UserExperienceDto>>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<UserExperienceDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var topUserExperiences = await _context
                .UserExperiences
                .Include(uxp => uxp.User)
                .OrderByDescending(uxp => uxp.Xp)
                .ToListAsync();

            return Result.Success(_mapper.Map<List<UserExperienceDto>>(topUserExperiences));
                
        }
    }
}
