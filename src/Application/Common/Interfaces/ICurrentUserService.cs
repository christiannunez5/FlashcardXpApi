using FlashcardXpApi.Domain;

namespace FlashcardXpApi.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Task<User?> GetCurrentUser();
    }
}
