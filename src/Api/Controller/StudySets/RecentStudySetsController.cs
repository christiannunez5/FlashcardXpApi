

using Application.Features.StudySets.Queries;
using Microsoft.AspNetCore.Mvc;
using Application.Extensions;
using Application.Features.StudySets.Commands;
using Application.Features.StudySets.Payloads;
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
    
    [HttpPut]
    public async Task<IResult> InsertNewRecentStudySets([FromBody] CreateRecentStudySetRequest request)
    {
        var command = new CreateRecentStudySet.Command
        {
            StudySetId = request.StudySetId
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }

}
