using FlashcardXpApi.Data;
using FlashcardXpApi.Features.StudySets;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Features.Flashcards
{
    public class FlashcardRepository : IFlashcardRepository
    {
        private readonly DataContext _context;

        public FlashcardRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Flashcard>> GetAllByStudySet(StudySet studySet)
        {
            return await _context.Flashcards
                .Where(f => f.StudySet == studySet)
                .ToListAsync();
        }

        public async Task<Flashcard?> GetByIdAsync(string id)
        {
            return await _context.Flashcards
                .Include(f => f.StudySet)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task InsertAsync(Flashcard flashcard)
        {
            _context.Flashcards.Add(flashcard);
            await _context.SaveChangesAsync();
        }

        public async Task InsertAllAsync(List<Flashcard> flashcards)
        {
            await _context.AddRangeAsync(flashcards);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Flashcard flashcard)
        {
            _context.Flashcards.Update(flashcard);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Flashcard flashcard)
        {
            _context.Flashcards.Remove(flashcard);
            await _context.SaveChangesAsync();
        }


    }
}
