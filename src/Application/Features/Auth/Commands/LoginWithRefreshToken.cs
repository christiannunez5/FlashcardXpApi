using Application.Common.Abstraction;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands;

public static class LoginWithRefreshToken
{
    public class Command : IRequest<Result>
    {

    };
    public class Handler : IRequestHandler<Command, Result>
    {

        private readonly ICookieService _cookieService;
        private readonly IApplicationDbContext _context;
        private readonly ITokenProvider _tokenProvider;
        
        public Handler(ICookieService cookieService, IApplicationDbContext context, ITokenProvider tokenProvider)
        {
            _cookieService = cookieService;
            _context = context;
            _tokenProvider = tokenProvider;
        }
        
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var cookieRefreshToken = _cookieService.Get("refreshToken");
            
            var refreshToken = await _context.RefreshTokens
                                .Include(r => r.User)
                                .FirstOrDefaultAsync(r => r.Token == cookieRefreshToken, cancellationToken);
            
            if (refreshToken == null)
            {
                return Result.Failure(AuthErrors.NotAuthorize);
            }

            if (refreshToken.User == null)
            {
                throw new InvalidOperationException("User cannot be null here");
            }
            
            refreshToken.ExpiresOnUtc =  DateTime.UtcNow.AddDays(14);
            await _context.SaveChangesAsync(cancellationToken);
            
            var newAccessToken = _tokenProvider.CreateToken(refreshToken.User);
            
            _cookieService.Store("accessToken", newAccessToken, DateTime.Now.AddMinutes(15));
            _cookieService.Store("refreshToken", refreshToken.Token, DateTime.Now.AddDays(14));

            return Result.Success();

        }
    }
}