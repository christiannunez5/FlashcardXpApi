using FlashcardXpApi.Features.Users;

namespace FlashcardXpApi.Services
{
    public interface ICurrentUserService
    {
        Task<User?> GetCurrentUser();
    }
}
