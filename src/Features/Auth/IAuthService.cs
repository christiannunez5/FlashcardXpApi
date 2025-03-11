using FlashcardXpApi.Common.Results;

namespace FlashcardXpApi.Features.Auth
{
    public interface IAuthService
    {
        Task<Result> Login(UserLoginRequest request);
        Task<Result> Register(CreateUserRequest request);

        Task<Result> LoginWithRefreshToken();

        Task<Result> Logout();
    }
}
