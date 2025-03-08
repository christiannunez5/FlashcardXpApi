using FlashcardXpApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.Features.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        /*
        [HttpGet("me")]
        [Authorize]
        public async Task<IResult> GetCurrentLoggedInUser()
        {
            var response = await _authService.GetLoggedInUserHttp();
            return response.ToHttpResponse();
        }
        */


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
