using FlashcardXpApi.Application.Common.Extensions;
using FlashcardXpApi.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.Application.Features.Flashcards
{
    public class FlashcardController : ApiControllerBase
    {

        [HttpPatch("{id}")]
        public async Task<IResult> Update([FromBody] UpdateFlashcardRequest request, string id)
        {
            var command = new UpdateFlashcard.Command
            { 
                Id = id,
                Term = request.Term,
                Definition = request.Definition
            };
            var response = await Mediator.Send(command);
            return response.ToHttpResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(string id)
        {
            var command = new DeleteFlashcard.Command
            { 
                Id = id 
            };

            var response = await Mediator.Send(command);
            return response.ToHttpResponse();
        }

    }
}
