

using Application.Features.StudySets.Queries;
using Microsoft.AspNetCore.Mvc;
using Application.Extensions;
using Application.Features.StudySets.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controller.StudySets;

[Route("/api/recent-study-sets")]
[Authorize]
public class RecentStudySetsController : ApiControllerBase
{

    [HttpGet]
    public async Task<IResult> GetRecentStudySets()
    {
        var query = new GetCurrentUserRecentStudySets.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }

    [HttpPost("{id}")]
    public async Task<IResult> InsertNewRecentStudySets(string id)
    {
        var command = new CreateRecentStudySet.Command
        {
            StudySetId = id
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }

}
