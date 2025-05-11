using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Users.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries;

public static class GetUserFollowing
{
    public class Query : IRequest<Result<List<UserDto>>>
    {

    };
    
    public class Handler : IRequestHandler<Query, Result<List<UserDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;

        public Handler(IMapper mapper, IApplicationDbContext context, IUserContext userContext)
        {
            _mapper = mapper;
            _context = context;
            _userContext = userContext;
        }
        
        public async Task<Result<List<UserDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var followings = await _context
                .UserFollowings
                .Where(uf => uf.UserId == _userContext.UserId())
                .Select(uf => uf.Following)
                .ToListAsync(cancellationToken);
                
            return Result.Success(_mapper.Map<List<UserDto>>(followings));
        }
    }
}