using Application.Extensions;
using Application.Features.Auth.Commands;
using Application.Features.Auth.Payloads;
using Application.Features.Auth.Queries;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Auth;

[Authorize]
public class AuthController : ApiControllerBase
{
    [HttpGet("me")]
    public async Task<IResult> GetCurrentUser()
    {
        var query = new GetCurrentLoggedInUser.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }

    [AllowAnonymous]
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


    [AllowAnonymous]
    [HttpPost("login/refresh-token")]
    public async Task<IResult> LoginRefreshToken()
    {
        var command = new LoginWithRefreshToken.Command();
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }

    [AllowAnonymous]
    [HttpPost("register")]
     public async Task<IResult> Register([FromBody] CreateUserRequest request)
     {
        var command = new CreateUser.Command
        {
            Email = request.Email,
            Username = request.Username,
            Password = request.Password,
            ProfilePic = request.ProfilePicUrl
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