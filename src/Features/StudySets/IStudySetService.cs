using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Features.Users;

namespace FlashcardXpApi.Features.StudySets
{
    public interface IStudySetService
    {
        Task<ResultGeneric<List<StudySetDto>>> GetStudySetsByUser();

        Task<ResultGeneric<string>> AddEmptyStudySet();

        Task<ResultGeneric<StudySetDto>> DeleteStudySet(string studySetId);
        Task<ResultGeneric<string>> UpdateStudySet(string studySetId, StudySetWithFlashcardsRequest request);

    }
}
