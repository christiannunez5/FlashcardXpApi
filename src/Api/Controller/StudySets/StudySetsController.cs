using Application.Extensions;
using Application.Features.StudySets.Commands;
using Application.Features.StudySets.Payloads;
using Application.Features.StudySets.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.StudySets;

[Authorize]
[Route("api/study-sets")]
public class StudySetsController : ApiControllerBase
{

    [HttpGet]
    public async Task<IResult> GetStudySets()
    {
        var query = new GetCurrentUserStudySets.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IResult> GetStudySet(string id)
    {
        var query = new GetStudySetById.Query
        {
            Id = id
        };
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
    [HttpPost("draft")]
    public async Task<IResult> AddDraftStudySet()
    {
        var command = new CreateDraftStudySet.Command();
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }

    [HttpPatch("{id}")]
    public async Task<IResult> UpdateStudySet([FromBody] StudySetRequest request, string id)
    {
        var command = new UpdateStudySetById.Command
        {
            Id = id,
            Title = request.Title,
            Description = request.Description
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }
    
    [HttpPatch("{id}/status")]
    public async Task<IResult> UpdateStatus(string id)
    {
        var command = new UpdateStudySetStatus.Command
        {
            Id = id,
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }


    [HttpDelete("{id}")]
    public async Task<IResult> DeleteStudySet(string id)
    {
        var command = new DeleteStudySetById.Command
        {
            Id = id
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }


}