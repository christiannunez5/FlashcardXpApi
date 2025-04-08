using FlashcardXpApi.Application.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.Application.Features.Quests
{
    [Authorize]
    public class QuestsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IResult> GetCurrentUserQuests()
        {
            var query = new GetCurrentUserQuests.Query { };
            var response = await Mediator.Send(query);
            return response.ToHttpResponse();
        }
        
        [HttpPatch("reset")]
        public async Task<IResult> ResetUserQuest()
        {
            var command = new ResetDailyQuests.Command();
            var response = await Mediator.Send(command);
            return response.ToHttpResponse();
        }

        [HttpPatch("{id}/complete")]

        public async Task<IResult> CompleteQuest([FromQuery] string id)
        {
            var command = new CompleteQuest.Command 
            { 
                Id = id 
            };
            var response = await Mediator.Send(command);
            return response.ToHttpResponse();
        }
    }
}
