using FlashcardXpApi.Application.Common.Extensions;
using FlashcardXpApi.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.Application.Features.Auth
{
    public class AuthController : ApiControllerBase
    {
        

        [HttpGet("me")]
        public async Task<IResult> GetCurrentLoggedInUser()
        {

            var query = new GetLoggedInUser.Query { };

            var response = await Mediator.Send(query);

            return response.ToHttpResponse();
           
        }

        [HttpPost("login/refresh-token")]
        public async Task<IResult> LoginRefreshToken()
        {
            var command = new LoginWithRefreshToken.Command();

            var response = await Mediator.Send(command);
            return response.ToHttpResponse();
        }

        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] LoginUserRequest request)
        {
            var command = new LoginUser.Command
            {
                Email = request.Email,
                Password = request.Password
            };

            var response = await Mediator.Send(command);

            return response.ToHttpResponse();
        }

        [HttpPost("register")]
        public async Task<IResult> Create([FromBody] CreateUserRequest request)
        {
            var command = new CreateUser.Command
            {
                Email = request.Email,
                Username = request.Username,
                Password = request.Password,
                ProfilePicUrl = request.ProfilePicUrl
            };

            var response = await Mediator.Send(command);

            return response.ToHttpResponse();
        }

        [HttpPost("logout")]
        public async Task<IResult> Logout()
        {
            var command = new LogoutUser.Command();

            var response = await Mediator.Send(command);
            return response.ToHttpResponse();
        }
        

    }
}
