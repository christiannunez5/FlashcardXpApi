using Application.Extensions;
using Application.Features.Flashcards.Commands;
using Application.Features.Flashcards.Payloads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller.Flashcards;

[Authorize]
public class FlashcardsController : ApiControllerBase
{

    [HttpPost("{id}")]
    public async Task<IResult> UpdateFlashcard([FromBody] UpdateFlashcardRequest request, string id)
    {
        var command = new UpdateFlashcardById.Command
        {
            Id = id,
            Term = request.Term,
            Definition = request.Definition
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }

    [HttpDelete("{id}")]
    public async Task<IResult> DeleteFlashcard(string id)
    {
        var command = new DeleteFlashcardById.Command
        {
            Id = id,
        };
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }

    [HttpPost]
    public async Task<IResult> AddNewFlashcard([FromBody] CreateFlashcardRequest request)
    {
        var command = new CreateFlashcard.Command
        {
            Term = request.Term,
            Definition = request.Definition,
            StudySetId = request.StudySetId
        };
        
        var response = await Mediator.Send(command);
        return response.ToHttpResponse();
    }


}
