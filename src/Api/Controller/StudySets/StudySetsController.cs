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
    
    [HttpPut("{id}/full")]
    public async Task<IResult> UpdateFullStudySet([FromBody] UpdateFullStudySetRequest request, string id)
    {
        var command = new UpdateFullStudySetById.Command
        {
            Id = id,
            Title = request.Title,
            Description = request.Description,
            Flashcards = request.Flashcards
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }

    [HttpPatch("{id}")]
    public async Task<IResult> UpdateStudySetBasicInfo([FromBody] UpdateStudySetBasicInfoRequest request, string id)
    {
        var command = new UpdateStudySetBasicInfoById.Command
        {
            Id = id,
            Title = request.Title,
            Description = request.Description,
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
    
    [HttpGet("{id}/ratings")]
    public async Task<IResult> GetStudySetRating(string id)
    {
        var command = new GetStudySetRatingById.Query()
        {
            StudySetId = id
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }
    
    [HttpPost("{id}/ratings")]
    public async Task<IResult> AddRating([FromBody] StudySetRatingRequest request, string id)
    {
        var command = new CreateStudySetRating.Command
        {
            StudySetId = id,
            Rating = request.Rating,
            ReviewText = request.ReviewText
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }
    
    [HttpPatch("{id}/ratings")]
    public async Task<IResult> UpdateRating([FromBody] StudySetRatingRequest request, string id)
    {
        var command = new UpdateStudySetRating.Command
        {
            StudySetId = id,
            Rating = request.Rating,
            ReviewText = request.ReviewText
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }
    
    [HttpGet("{id}/ratings/me")]
    public async Task<IResult> DidUserAlreadyReviewed(string id)
    {
        var command = new GetUserStudySetRating.Query
        {
            StudySetId = id,
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }

}