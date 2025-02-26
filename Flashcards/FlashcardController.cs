using FlashcardXpApi.Extensions;
using FlashcardXpApi.Flashcards.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.Flashcards
{
    [ApiController]
    [Route("/api")]
    public class FlashcardController : ControllerBase
    {
        private readonly FlashcardService _flashcardService;

        public FlashcardController(FlashcardService flashcardService)
        {
            _flashcardService = flashcardService;
        }

        /*
        [HttpGet("{studySetId}/flashcards")]
        public async Task<IResult> GetByStudySet(int studySetId)
        {
            var response = await _flashcardService.GetAllByStudySet(studySetId);
            return response.ToHttpResponse();
        }

        [HttpPost("{studySetId}/flashcards")]
        public async Task<IResult> AddNewFlashcard(
            int studySetId,
            List<FlashcardRequest> request
        )
        {
          
            var response = await _flashcardService.AddNewFlashcard(studySetId, request);
            return response.ToHttpResponse();
            
        }
        
        [HttpDelete("flashcards/{flashcardId}")]
        public async Task<IResult> DeleteFlashcard(int flashcardId)
        {
            var response = await _flashcardService.DeleteFlashcard(flashcardId); 
            return response.ToHttpResponse();
        }

        /*
        [HttpGet("flashcards/{flashcardId}")]
        public async Task<IResult> GetFlashcard(int flashcardId)
        {
            var response = await _flashcardService.Get
        }

        */



    }
}
