
using FlashcardXpApi.Users;

namespace FlashcardXpApi.FlashcardSets
{
    public interface IStudySetRepository
    {
        Task<List<StudySet>> GetAllAsync();

        Task<StudySet?> GetByIdAsync(int id);

        Task<List<StudySet>> GetAllByUserId(int id);

        Task InsertAsync(StudySet studySet);
            
        Task UpdateAsync(StudySet studySet);

        Task DeleteAsync(StudySet studySet);

    }
}
