using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Features.Users;

namespace FlashcardXpApi.Features.StudySets
{
    public interface IStudySetService
    {
        Task<ResultGeneric<List<StudySetDto>>> GetStudySetsByUser();

        Task<ResultGeneric<string>> AddNewStudySetWithFlashcards(StudySetWithFlashcardsRequest request);

        Task<ResultGeneric<StudySetDto>> DeleteStudySet(string studySetId);
        Task<ResultGeneric<StudySetDto>> UpdateStudySet(string studySetId, StudySetWithFlashcardsRequest request);

    }
}
