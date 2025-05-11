using Application.Common.Models;
using Application.Features.Auth.Payloads;
using Application.Features.Users.Payloads;
using AutoMapper;
using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries;

public static class GetUserByEmailOrUsername
{
    public class Query : IRequest<Result<List<UserDto>>>
    {
        public required string Value { get; init; }
    }
    
    public class Handler : IRequestHandler<Query, Result<List<UserDto>>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public Handler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<List<UserDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var users = await _userManager
                .Users
                .Where(u =>
                    (u.Email != null && u.Email.Contains(request.Value)) ||
                    (u.UserName != null && u.UserName.Contains(request.Value)))
                .ToListAsync(cancellationToken);
            
            return Result.Success(_mapper.Map<List<UserDto>>(users));

        }
    }
}