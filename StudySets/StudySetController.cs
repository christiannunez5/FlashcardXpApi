using FlashcardXpApi.Extensions;
using FlashcardXpApi.FlashcardSets;
using FlashcardXpApi.StudySets.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.StudySets
{
    [ApiController]
    [Route("api/")]
    public class StudySetController : ControllerBase
    {

        private readonly StudySetService _studySetService;

        public StudySetController(StudySetService flashcardSetService)
        {
            _studySetService = flashcardSetService;
        }

        [HttpGet("user/{id}/studyset")]
        public async Task<IResult> GetAllByUserId(int id)
        {
            var response = await _studySetService.GetAllByUserId(id);
            return response.ToHttpResponse();
        }

        [HttpPost("user/{userId}/studyset")]
        public async Task<IResult> Add(int userId, StudySetRequest request)
        {
            var response = await _studySetService.AddNewStudySet(userId, request);
            return response.ToHttpResponse();
        }

        [HttpPatch("user/{userId}/studyset/{studySetId}")]
        public async Task<IResult> UpdateStudySet(
           int userId,
           int studySetId,
           StudySetRequest request
        )
        {
            var response = await _studySetService.UpdateStudySet(userId, studySetId, request);
            return response.ToHttpResponse();
        }

        [HttpDelete("user/{userId}/studyset/{studySetId}")]
        public async Task<IResult> UpdateStudySet(
           int userId,
           int studySetId
        )
        {
            var response = await _studySetService.DeleteStudySet(userId, studySetId);

            return response.ToHttpResponse();
        }

        [HttpGet]
        public void GetById()

        {

        }
    }
}
