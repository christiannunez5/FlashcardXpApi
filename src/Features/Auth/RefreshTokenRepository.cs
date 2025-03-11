using FlashcardXpApi.Data;
using FlashcardXpApi.Features.Users;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Features.Auth
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {

        private readonly DataContext _context;

        public RefreshTokenRepository(DataContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(RefreshToken token)
        {
            _context.RefreshTokens.Remove(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByToken(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == token);

            return refreshToken;
        }

        public async Task InsertAsync(RefreshToken token)
        {
            
            _context.RefreshTokens.Add(token);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }
    }
}
