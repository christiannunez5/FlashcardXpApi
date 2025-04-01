using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Contracts;
using FlashcardXpApi.Domain;
using FlashcardXpApi.Infrastructure.Persistence;
using FlashcardXpApi.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.Auth
{
    
    public static class LoginUser
    {

        public class Command : IRequest<Result>
        {
            public required string Email { get; set; }
            public required string Password { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly SignInManager<User> _signInManager;
            private readonly UserManager<User> _userManager;
            private readonly JwtHandler _jwtHandler;
            private readonly ICookieService _cookieService;
            private readonly DataContext _context;

            public Handler(SignInManager<User> signInManager,
                UserManager<User> userManager,
                ICookieService cookieService,
                JwtHandler jwtHandler,
                DataContext context)
            {
                _signInManager = signInManager;
                _userManager = userManager;
                _cookieService = cookieService;
                _jwtHandler = jwtHandler;
                _context = context;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    return Result.Failure(AuthErrors.UserNotFoundError);
                }

                var signInResult = await _signInManager.
                    CheckPasswordSignInAsync(user, request.Password, false);

                if (!signInResult.Succeeded)
                {
                    return Result.Failure(AuthErrors.InvalidLoginCredentials);
                }

                var accessToken = _jwtHandler.CreateToken(user);
                var refreshToken = _jwtHandler.GenerateRefreshToken();

                var newRefreshToken = new RefreshToken
                {
                    Token = refreshToken,
                    UserId = user.Id,
                    ExpiresOnUtc = DateTime.UtcNow.AddDays(14)
                };

                _context.RefreshTokens.Add(newRefreshToken);
                await _context.SaveChangesAsync();

                _cookieService.Store("accessToken", accessToken, DateTime.Now.AddMinutes(15));
                _cookieService.Store("refreshToken", refreshToken, DateTime.Now.AddDays(14));
    
                return Result.Success();
            }

        }

    }
    

}
