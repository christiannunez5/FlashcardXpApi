using FlashcardXpApi.Features.Users;

namespace FlashcardXpApi.Features.Auth
{
    public interface ICurrentUserService
    {
        Task<User?> GetCurrentUser();
    }
}
