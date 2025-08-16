using Application.Extensions;
using Application.Features.Users.Command;
using Application.Features.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Users;

[Authorize]
public class UsersController : ApiControllerBase
{
    
    [HttpGet("top-creators")]
    public async Task<IResult> GetTopStudySetCreators()
    {
        var query = new GetTopStudySetCreator.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
    [HttpGet("top-xp-earners")]
    public async Task<IResult> GetTopPlayersByXp()
    {
        var query = new GetTopPlayersByXp.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
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

    [HttpPost("{id}/follow")]
    public async Task<IResult> FollowUser(string id)
    {
        var command = new CreateUserFollowing.Command
        {
            UserToFollowId = id
        };
        
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }
    
    [HttpGet("followers")]
    public async Task<IResult> GetFollowers()
    {
        var query = new GetUserFollowers.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
    [HttpGet("followings")]
    public async Task<IResult> GetFollowings()
    {
        var query = new GetUserFollowing.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
    [HttpGet("{id}/is-followed")]
    public async Task<IResult> IsUserFollowed(string id)
    {
        var query = new IsUserAlreadyFollowed.Query
        {
            UserFollowingId = id
        };
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
}