using FlashcardXpApi.Data;
using FlashcardXpApi.Features.Users;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Features.StudySets
{
    public class StudySetRepository : IStudySetRepository
    {
        private readonly DataContext _context;

        public StudySetRepository(DataContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(StudySet studySet)
        {
            _context.Add(studySet);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StudySet>> GetAllAsync()
        {
            return await _context.StudySets.ToListAsync();
        }

        public async Task<List<StudySet>> GetAllByUser(User user)
        {
            return await _context.StudySets
                .Include(s => s.Flashcards)
                .Where(s => s.CreatedById == user.Id)
                .ToListAsync();
        }

        public async Task<StudySet?> GetByIdAsync(string id)
        {
            return await _context.StudySets
                .Include(s => s.StudySetParticipants)
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
