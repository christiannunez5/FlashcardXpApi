using FlashcardXpApi.Application.Common.Extensions;
using FlashcardXpApi.Application.Contracts.UserExperience;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.Application.Features.Experience;

[Route("api/user-experience")]
public class UserExperienceController : ApiControllerBase
{

    [HttpGet]
    public async Task<IResult> GetCurrentUserExperience()
    {
        var query = new GetCurrentUserExperience.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
    
    [HttpPatch]
    public async Task<IResult> IncrementUserExperience([FromBody] IncrementUserExperienceRequest request)
    {
        var command = new IncrementUserExperience.Command()
        {
            Xp = request.Xp,
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }
    
}