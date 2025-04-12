using Application.Extensions;
using Application.Features.UserExperiences.Commands;
using Application.Features.UserExperiences.Payloads;
using Application.Features.UserExperiences.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.UserExperiences;

[Authorize]
[Route("/api/user-experiences")]
public class UserExperiencesController : ApiControllerBase
{

    [HttpGet]
    public async Task<IResult> GetExperience()
    {
        var query = new GetCurrentUserExperience.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
    [HttpPatch]
    public async Task<IResult> IncrementExperience(
                [FromBody] IncrementUserExperienceRequest request)
    {
        var command = new IncrementUserExperience.Command
        {
            UserQuestId = request.UserQuestId
        };

        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }




}
