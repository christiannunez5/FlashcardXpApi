using Application.Extensions;
using Application.Features.Quests.Queries;
using Application.Features.UserQuests.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.UserQuests;

[Route("api/user-quests")]
[Authorize]
public class UserQuestsController : ApiControllerBase
{
    
    [HttpGet]
    public async Task<IResult> GetQuests()
    {
        var query = new GetCurrentUserQuests.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }

    [HttpPatch("{id}/complete")]
    public async Task<IResult> CompleteQuest(string id)
    {
        var command = new MarkQuestAsComplete.Command
        {
            Id = id
        };

        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }
    
    [HttpPatch("reset")]
    public async Task<IResult> Reset()
    {
        var command = new ResetDailyQuests.Command();
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }



}
