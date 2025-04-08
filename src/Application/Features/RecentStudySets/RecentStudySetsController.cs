using FlashcardXpApi.Application.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.Application.Features.RecentStudySets
{
    [Authorize]
    [Route("api/recent-studysets")]
    public class RecentStudySetsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IResult> GetAll()
        {
            var query = new GetCurrentUserRecentStudySets.Query { };
            var response = await Mediator.Send(query);
            return response.ToHttpResponse();
        }

        [HttpPost("{id}")]
        public async Task<IResult> AddNewRecentStudySet(string id)
        {
            var command = new CreateRecentStudySet.Command { Id = id };
            var response = await Mediator.Send(command);
            return response.ToHttpResponse();
        }

    }
}
