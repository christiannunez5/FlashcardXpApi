

using Application.Common.Abstraction;
using Application.Common.Models;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands;

public static class LogoutUser
{
    public class Command : IRequest<Result>
    {

    }

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICookieService _cookieService;
        
        public Handler(IApplicationDbContext context, ICookieService cookieService)
        {
            _context = context;
            _cookieService = cookieService;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var cookieToken = _cookieService.Get("refreshToken");

            var token = await _context.RefreshTokens
                     .FirstOrDefaultAsync(r => r.Token == cookieToken, cancellationToken);

            if (token != null) 
            {
                _context.RefreshTokens.Remove(token);
                await _context.SaveChangesAsync(cancellationToken);
            }

            _cookieService.Store("refreshToken", "", DateTime.Now.AddMinutes(-1));
            _cookieService.Store("accessToken", "", DateTime.Now.AddMinutes(-1));
            return Result.Success();
        }
    }
}
