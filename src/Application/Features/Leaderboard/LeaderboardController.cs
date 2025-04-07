using FlashcardXpApi.Application.Common.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.Application.Features.Leaderboard;

public class LeaderboardController : ApiControllerBase
{
    
    [HttpGet]
    public async Task<IResult> GetLeaderboard()
    {
        var query = new GetLeaderboardByExperience.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
}