
using FlashcardXpApi.Users;

namespace FlashcardXpApi.FlashcardSets
{
    public interface IStudySetRepository
    {
        Task<List<StudySet>> GetAllAsync();

        Task<StudySet?> GetByIdAsync(string id);

        Task<List<StudySet>> GetAllByUser(User user);

        Task InsertAsync(StudySet studySet);
            
        Task UpdateAsync(StudySet studySet);

        Task DeleteAsync(StudySet studySet);


    }
}
