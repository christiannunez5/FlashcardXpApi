using FlashcardXpApi.Application.Common.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.Application.Features.Quests
{
    public class QuestsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IResult> GetQuestsByUser()
        {
            var query = new GetUserQuests.Query { };
            var response = await Mediator.Send(query);
            return response.ToHttpResponse();
        }
    }
}
