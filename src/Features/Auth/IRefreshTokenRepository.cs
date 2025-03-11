using FlashcardXpApi.Features.Users;

namespace FlashcardXpApi.Features.Auth
{
    public interface IRefreshTokenRepository
    {
        Task InsertAsync(RefreshToken token);

        Task<RefreshToken?> GetByToken(string token);
        Task UpdateAsync(RefreshToken token);

        Task DeleteAsync(RefreshToken token);

    }
}
