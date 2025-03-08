using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Features.Users;

namespace FlashcardXpApi.Features.StudySets
{
    public interface IStudySetService
    {
        Task<ResultGeneric<List<StudySetDto>>> GetStudySetsByUser();

        Task<ResultGeneric<StudySetDto>> AddNewStudySet(StudySetRequest request);

        Task<ResultGeneric<StudySetDto>> DeleteStudySet(string studySetId);
        Task<ResultGeneric<StudySetDto>> UpdateStudySet(string studySetId, StudySetRequest request);

    }
}
