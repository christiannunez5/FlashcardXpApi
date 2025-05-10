using Application.Extensions;
using Application.Features.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Users;

[Authorize]
public class UsersController : ApiControllerBase
{
    [HttpGet("search")]
    public async Task<IResult> GetUserByEmailOrUsername([FromQuery] string value)
    {
        var query = new GetUserByEmailOrUsername.Query
        {
            Value = value
        };
        
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
}