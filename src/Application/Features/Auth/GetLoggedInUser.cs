using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Contracts;
using FlashcardXpApi.Application.Contracts.Auth;
using MediatR;

namespace FlashcardXpApi.Application.Features.Auth
{
    public static class GetLoggedInUser
    {
        public class Query : IRequest<Result<UserResponse>>
        {

        };

        public class Handler : IRequestHandler<Query, Result<UserResponse>>
        {
            private readonly ICurrentUserService _currentUserService;

            public Handler(ICurrentUserService currentUserService)
            {
                _currentUserService = currentUserService;
            }

            public async Task<Result<UserResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _currentUserService.GetCurrentUser();
                
                if (user is null)
                {
                    return Result.Failure<UserResponse>(AuthErrors.AuthenticationRequiredError);
                }
                
                UserResponse userDto = new UserResponse
                (
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.ProfilePicUrl
                );
                
                return Result.Success(userDto);

            }
        }
    }
}
