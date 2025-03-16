using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.Auth
{
    public static class LogoutUser
    {
        public class Command : IRequest<Result>
        { 

        };

        public class Handler : IRequestHandler<Command, Result> 
        {
            private readonly DataContext _context;
            private readonly ICookieService _cookieService;

            public Handler(DataContext context, ICookieService cookieService)
            {
                _context = context;
                _cookieService = cookieService;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {

                var cookieToken = _cookieService.Get("refreshToken");

                var token = await _context.RefreshTokens
                        .FirstOrDefaultAsync(r => r.Token == cookieToken);

                if (token is not null)
                {
                    _context.RefreshTokens.Remove(token);
                    await _context.SaveChangesAsync(cancellationToken);
                }

                _cookieService.Remove("accessToken");
                _cookieService.Remove("refreshToken");

                return Result.Success();
            }
        };
    }
}
