using FlashcardXpApi.FlashcardSets;

namespace FlashcardXpApi.Flashcards
{
    public interface IFlashcardRepository
    {
        Task<Flashcard?> GetByIdAsync(int id);
        Task<List<Flashcard>> GetAllByStudySet(StudySet studySet);
        Task InsertAsync(Flashcard flashcard);
        Task DeleteAsync(Flashcard flashcard);
        Task UpdateAsync(Flashcard flashcard);
    }
}
