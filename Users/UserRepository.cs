using FlashcardXpApi.Data;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public void Insert(User user)
        {
            _context.Users.Add(user);
        }

        public async Task<bool> IsEmailUnique(string email)
        {
            return !await _context.Users.AnyAsync(u => u.Email == email);
        }

        public void SaveChangesAsync()
        {
            _context.SaveChangesAsync();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
