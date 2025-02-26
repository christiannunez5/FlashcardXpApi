using FlashcardXpApi.Auth.Requests;
using FlashcardXpApi.Extensions;
using FlashcardXpApi.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Net;

namespace FlashcardXpApi.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IResult> GetCurrentLoggedInUser()
        {
            var response = await _authService.GetLoggedInUserHttp();
            return response.ToHttpResponse();
        }
        
        [HttpPost("register")]
        public async Task<IResult> Register (
            CreateUserRequest request
        )
        {
            var response = await _authService.Register(request);
            return response.ToHttpResponse();
        }

        [HttpPost("login")]
        public async Task<IResult> Login(UserLoginRequest request)
        {
            var response = await _authService.Login(request, HttpContext);
            return response.ToHttpResponse();
        }



    }
}
