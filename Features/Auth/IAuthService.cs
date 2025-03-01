using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Features.Auth.Requests;

namespace FlashcardXpApi.Features.Auth
{
    public interface IAuthService
    {
        Task<Result> Login(UserLoginRequest request);
        Task<Result> Register(CreateUserRequest request);

    }
}
