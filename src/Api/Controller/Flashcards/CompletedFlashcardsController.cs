using Application.Extensions;
using Application.Features.CompletedFlashcards.Commands;
using Application.Features.CompletedFlashcards.Payloads;
using Application.Features.CompletedFlashcards.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Flashcards;

[Authorize]
[Route("/api/completed-flashcards")]
public class CompletedFlashcardsController : ApiControllerBase
{

    [HttpGet]
    public async Task<IResult> GetCompletedFlashcardsToday()
    {
        var query = new GetCurrentUserCompletedFlashcards.Query();
        var response = await Mediator.Send(query);
        return response.ToHttpResponse();
    }

    [HttpPost]
    public async Task<IResult> AddNewCompletedFlashcard([FromBody] CreateCompletedFlashcardRequest request)
    {
        var command = new CreateCompletedFlashcard.Command
        {
            FlashcardId = request.FlashcardId
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }

}
