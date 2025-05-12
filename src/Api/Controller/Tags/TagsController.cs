using Application.Extensions;
using Application.Features.Tags.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Tags;

[Authorize]
public class TagsController : ApiControllerBase
{
    [HttpGet]
    public async Task<IResult> GetTags()
    {
        var query = new GetTags.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
    [HttpGet("{id}/study-sets")]
    public async Task<IResult> GetStudySetsByTag(string id)
    {
        var query = new GetStudySetsByTagId.Query
        {
            TagId = id
        };
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
}