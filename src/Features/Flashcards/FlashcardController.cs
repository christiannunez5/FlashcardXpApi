using FlashcardXpApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.Features.Flashcards
{
    [ApiController]
    [Route("/api")]
    [Authorize]
    public class FlashcardController : ControllerBase
    {
        private readonly FlashcardService _flashcardService;

        public FlashcardController(FlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }


        [AllowAnonymous]
        [HttpGet("studysets/{studySetId}/flashcards")]
        public async Task<IResult> GetByStudySet(string studySetId)
        {
            var response = await _flashcardService.GetAllByStudySet(studySetId);
            return response.ToHttpResponse();
        }

        [HttpPost("studysets/{studySetId}/flashcards")]
        public async Task<IResult> AddNewFlashcard(
            string studySetId,
            List<FlashcardRequest> request
        )
        {

            var response = await _flashcardService.AddNewFlashcard(studySetId, request);
            return response.ToHttpResponse();

        }

        [HttpDelete("flashcards/{flashcardId}")]
        public async Task<IResult> DeleteFlashcard(string flashcardId)
        {
            var response = await _flashcardService.DeleteFlashcard(flashcardId);
            return response.ToHttpResponse();
        }


        /*
        [HttpGet("flashcards/{flashcardId}")]
        public async Task<IResult> GetFlashcard(string flashcardId)
        {
            var response = await _flashcardService.Get
        }

        
        */



    }
}
