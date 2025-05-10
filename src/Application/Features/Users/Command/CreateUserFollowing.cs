using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Auth.Payloads;
using Application.Features.Users.Payloads;
using AutoMapper;
using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Command;

public static class CreateUserFollowing
{
    public class Command : IRequest<Result<UserDto>>
    {
        public required string UserToFollowId { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<UserDto>>
    {
        
        private readonly IUserContext _userContext;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        
        public Handler(IUserContext userContext, IApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _userContext = userContext;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Result<UserDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userToFollow = await _userManager
                .FindByIdAsync(request.UserToFollowId);

            if (userToFollow == null)
            {
                return Result.Failure<UserDto>(UserErrors.UserNotFound);
            }
            
            var newUserFollowing = new UserFollowing
            {
                UserId = _userContext.UserId(),
                FollowingId = userToFollow.Id
            };
                
            _context.UserFollowings.Add(newUserFollowing);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(_mapper.Map<UserDto>(userToFollow));
        }
    }
}