using FlashcardXpApi.Auth;
using FlashcardXpApi.Extensions;
using FlashcardXpApi.FlashcardSets;
using FlashcardXpApi.StudySets.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashcardXpApi.StudySets
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudySetController : ControllerBase
    {

        private readonly StudySetService _studySetService;

        public StudySetController(StudySetService flashcardSetService)
        {
            _studySetService = flashcardSetService;
        }

        
        [HttpGet]
        public async Task<IResult> GetStudySetsByUser()
        {
            var response = await _studySetService.GetAllByUser();
            return response.ToHttpResponse();
        }
            
        [HttpPost]
        public async Task<IResult> Add(StudySetRequest request)
        {
            var response = await _studySetService.AddNewStudySet(request);
            return response.ToHttpResponse();
        }
            
        [HttpPost("{studySetId}/user/{userId}")]
        public async Task<IResult> AddUserToStudySet(
          string studySetId,
          string userId
        )
        {
            var response = await _studySetService.AddUserToStudySet(studySetId, userId);
            return response.ToHttpResponse();
        }
        
        [HttpPatch("{studySetId}")]
        public async Task<IResult> UpdateStudySet (
          string studySetId,
          StudySetRequest request
        )
        {
            var response = await _studySetService.UpdateStudySet(studySetId, request);
            return response.ToHttpResponse();
        }
            
        [HttpDelete("{studySetId}")]
        public async Task<IResult> DeleteStudySet (
          string studySetId
        )
        {
            var response = await _studySetService.DeleteStudySet(studySetId);
            return response.ToHttpResponse();
        }
        
        /*

        [HttpGet("studyset/{id}")]
        public async Task<IResult> GetById(int id)
        {
            var response = await _studySetService.GetStudySet(id);
            return response.ToHttpResponse();
        }
        
       

        [HttpDelete("user/{userId}/studyset/{studySetId}")]
        public async Task<IResult> DeleteStudySet(
           string userId,
           int studySetId
        )
        {
            var response = await _studySetService.DeleteStudySet(userId, studySetId);
            return response.ToHttpResponse();
        }
        
        */
    }
}
