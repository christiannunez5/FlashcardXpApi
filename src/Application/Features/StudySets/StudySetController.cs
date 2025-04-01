using FlashcardXpApi.Application.Common.Extensions;
using FlashcardXpApi.Application.Contracts;
using FlashcardXpApi.Application.Features.RecentStudySets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.Application.Features.StudySets
{
    [Authorize]
    public class StudySetController : ApiControllerBase
    {
            
        [HttpGet]
        public async Task<IResult> GetAll()
        {
            var query = new GetCurrentUserStudySets.Query { };
            var result = await Mediator.Send(query);
            return result.ToHttpResponse();
        }

        [HttpGet("{id}")]
        public async Task<IResult> Get(string id)
        {
            var query = new GetStudySet.Query { Id = id  };
            var response = await Mediator.Send(query); 
            return response.ToHttpResponse();
        }

        [HttpPost]
        public async Task<IResult> CreateEmpty()
        {
            var command = new CreateDraftStudySet.Command();
            var response = await Mediator.Send(command);
            return response.ToHttpResponse();
        }

        [HttpPut("{id}")]
        public async Task<IResult> Update([FromBody] UpdateStudySetRequest request,
            [FromRoute] string id)
        {

            var command = new UpdateStudySet.Command
            {
                Id = id,
                Title = request.Title,
                Description = request.Description,
                Flashcards = request.Flashcards
            };
            
            var response = await Mediator.Send(command);
            return response.ToHttpResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete(string id)
        {
            var command = new DeleteStudySet.Command
            { 
                Id = id 
            };
            var response = await Mediator.Send(command);
            return response.ToHttpResponse();
        }

    }
}
