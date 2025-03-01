using FlashcardXpApi.Users;

namespace FlashcardXpApi.Auth.Interfaces
{
    public interface ICurrentUserService
    {
        Task<User?> GetCurrentUser();
    }
}
