using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Auth.Payloads;
using AutoMapper;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Queries;

public static class GetCurrentLoggedInUser
{
    public class Query : IRequest<Result<UserDto>>
    {

    };
    
    public class Handler : IRequestHandler<Query, Result<UserDto>>
    {
        private readonly IUserContext  _userContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        
        public Handler(IUserContext userContext, UserManager<User> userManager, IMapper mapper)
        {
            _userContext = userContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_userContext.UserId());
            return Result.Success(_mapper.Map<UserDto>(user));
        }
    }
}