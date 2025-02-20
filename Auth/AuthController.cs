using FlashcardXpApi.Auth.Requests;
using FlashcardXpApi.Extensions;
using FlashcardXpApi.Users;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

        

        [HttpPost]
        public async Task<IResult> Register(
            CreateUserRequest request
        )
        {
            var response = await _authService.Register(request);
            return response.ToHttpResponse();
        }

       
        
    }
}
