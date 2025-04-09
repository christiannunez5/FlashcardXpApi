using Application.Extensions;
using Application.Features.StudySets.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.StudySets;


[Route("api/study-sets")]
public class StudySetsController : ApiControllerBase
{

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
    
}