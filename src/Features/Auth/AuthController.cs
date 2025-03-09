using AutoMapper;
using FlashcardXpApi.Extensions;
using FlashcardXpApi.Features.Users;
using FlashcardXpApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.Features.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public AuthController(IAuthService authService,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _authService = authService;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }


        [Authorize]
        [HttpGet("me")]
        public async Task<IResult> GetCurrentLoggedInUser()
        {
            var user = await _currentUserService.GetCurrentUser();
            return Results.Ok(_mapper.Map<UserDto>(user));
        }


        [HttpPost("register")]
        public async Task<IResult> Register(
            CreateUserRequest request
        )
        {
            var response = await _authService.Register(request);
            return response.ToHttpResponse();
        }

        [HttpPost("login")]
        public async Task<IResult> Login(UserLoginRequest request)
        {
            var response = await _authService.Login(request);

            return response.ToHttpResponse();
        }



    }
}
