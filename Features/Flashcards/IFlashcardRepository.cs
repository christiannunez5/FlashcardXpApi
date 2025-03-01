using FlashcardXpApi.Features.StudySets;

namespace FlashcardXpApi.Features.Flashcards
{
    public interface IFlashcardRepository
    {
        Task<Flashcard?> GetByIdAsync(string id);
        Task<List<Flashcard>> GetAllByStudySet(StudySet studySet);
        Task InsertAsync(Flashcard flashcard);
        Task DeleteAsync(Flashcard flashcard);
        Task UpdateAsync(Flashcard flashcard);

        Task InsertAllAsync(List<Flashcard> flashcards);
    }
}
