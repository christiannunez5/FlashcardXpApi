using FlashcardXpApi.Data;
using FlashcardXpApi.Users;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.FlashcardSets
{
    public class StudySetRepository : IStudySetRepository
    {
        private readonly DataContext _context;

        public StudySetRepository(DataContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(StudySet flashcardSet)
        {
            _context.Add(flashcardSet);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StudySet>> GetAllAsync()
        {
            return await _context.StudySets.ToListAsync();
        }

        public async Task<List<StudySet>> GetAllByUserId(int id)
        {
            return await _context.StudySets
                .Where(fs => fs.CreatedById == id)
                .ToListAsync();
        }

        public async Task<StudySet?> GetByIdAsync(int id)
        {
            return await _context.StudySets
                .FirstOrDefaultAsync(fs => fs.Id == id);
        }

        public async Task UpdateAsync(StudySet studySet)
        {
            _context.StudySets.Update(studySet);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(StudySet studySet)
        {
            _context.StudySets
                .Remove(studySet);

            await _context.SaveChangesAsync();
        }
    }
}
