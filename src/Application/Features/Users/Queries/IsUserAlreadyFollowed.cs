using Application.Common.Abstraction;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries;

public static class IsUserAlreadyFollowed
{
    public class Query : IRequest<Result<bool>>
    {
        public required string UserFollowingId { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Result<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;

        public Handler(IApplicationDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<Result<bool>> Handle(Query request, CancellationToken cancellationToken)
        {
            var didUserFollow = await _context
                .UserFollowings
                .AnyAsync(uf => uf.UserId == _userContext.UserId() &&
                                uf.FollowingId == request.UserFollowingId, cancellationToken);

            return Result.Success(didUserFollow);
        }
    }
}