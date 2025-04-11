using Application.Extensions;
using Application.Features.Quests.Queries;
using Application.Features.UserQuests.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.UserQuests;

[Route("api/user-quests")]
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



}
