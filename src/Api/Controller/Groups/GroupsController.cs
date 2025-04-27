using Application.Extensions;
using Application.Features.Groups.Commands;
using Application.Features.Groups.Payloads;
using Application.Features.Groups.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Groups;

[Authorize]
public class GroupsController : ApiControllerBase
{
    [HttpGet]
    public async Task<IResult> GetGroups()
    {
        var query = new GetCurrentUserGroups.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
    [HttpPost]
    public async Task<IResult> CreateGroup([FromBody] CreateGroupRequest request)
    {
        var command = new CreateGroup.Command
        {
            Name = request.Name
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }
    
    [HttpPost("add-member")]
    public async Task<IResult> AddMember([FromBody] AddMemberRequest request)
    {
        var command = new AddMember.Command
        {
            GroupId = request.GroupId,
            UserId =  request.UserId
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }
    
}