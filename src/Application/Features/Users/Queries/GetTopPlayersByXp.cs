using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.UserExperiences.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries;

public static class GetTopPlayersByXp
{
    public class Query : IRequest<Result<List<UserExperienceDto>>>
    {
        
    }
    
    public class Handler : IRequestHandler<Query, Result<List<UserExperienceDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public Handler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<List<UserExperienceDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var topPlayers = await _context
                .UserExperiences
                .Include(ux => ux.User)
                .OrderByDescending(ux => ux.Xp)
                .Take(3)
                .ToListAsync(cancellationToken);
            
            return Result.Success(_mapper.Map<List<UserExperienceDto>>(topPlayers));
            
        }
    }
}