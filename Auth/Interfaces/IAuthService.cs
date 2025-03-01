using FlashcardXpApi.Auth.Requests;
using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Users;
using Microsoft.AspNetCore.Identity.Data;

namespace FlashcardXpApi.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<Result> Login(UserLoginRequest request);
        Task<Result> Register(CreateUserRequest request);

    }
}
