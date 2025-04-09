using FlashcardXpApi.Application.Common.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.Application.Features.FlashcardsCompletion;

[Route(("/api/flashcards-completion"))]
public class FlashcardsCompletionController : ApiControllerBase
{

    [HttpGet]
    public async Task<IResult> GetCompletedFlashcardsToday()
    {
        var query = new GetCurrentUserCompletedFlashcards.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }
         
    [HttpPost("{id}")]
    public async Task<IResult> CreateNewCompletedFlashcard(string id)
    {
        var command = new CreateCompletedFlashcard.Command
        {
            FlashcardId = id
        };
        
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }
    
}