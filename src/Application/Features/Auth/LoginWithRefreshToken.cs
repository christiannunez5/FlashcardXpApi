using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Infrastructure.Persistence;
using FlashcardXpApi.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.Auth
{
    public class LoginWithRefreshToken
    {
        public class Command : IRequest<Result>
        {

        };

        public class Handler : IRequestHandler<Command, Result>
        {

            private readonly DataContext _context;
            private readonly ICookieService _cookieService;
            private readonly JwtHandler _jwtHandler;
            public Handler(DataContext context, ICookieService cookieService, JwtHandler jwtHandler)
            {
                _context = context;
                _cookieService = cookieService;
                _jwtHandler = jwtHandler;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var cookieRefreshToken = _cookieService.Get("refreshToken");

                var refreshToken = await _context.RefreshTokens
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.Token == cookieRefreshToken);

                if (refreshToken is null)
                {
                    return Result.Failure(AuthErrors.AuthorizationFailedError);
                }

                if (refreshToken.User is null)
                {
                    throw new InvalidOperationException("User cannot be null");
                }

                var accessToken = _jwtHandler.CreateToken(refreshToken.User);

                refreshToken.Token = _jwtHandler.GenerateRefreshToken();
                refreshToken.ExpiresOnUtc = DateTime.Now.AddDays(14);

                _context.RefreshTokens.Update(refreshToken);
                await _context.SaveChangesAsync();
                    
                _cookieService.Store("accessToken", accessToken, DateTime.Now.AddMinutes(15));
                _cookieService.Store("refreshToken", refreshToken.Token, DateTime.Now.AddDays(14));
                    
                return Result.Success();
            }
        }

    }
}
