using Application.Common.Abstraction;
using Application.Common.Models;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands;

public static class LoginUser
{
    public class Command : IRequest<Result>
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
    
    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ICookieService _cookieService;
        private readonly ITokenProvider _tokenProvider;
        private readonly UserManager<User> _userManager;
        private readonly IApplicationDbContext _context;

        public Handler(SignInManager<User> signInManager, 
            ICookieService cookieService, ITokenProvider tokenProvider, UserManager<User> userManager, IApplicationDbContext context)
        {
            _signInManager = signInManager;
            _cookieService = cookieService;
            _tokenProvider = tokenProvider;
            _userManager = userManager;
            _context = context;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Result.Failure(AuthErrors.InvalidLoginCredentials);
            }
            
            var signInResult = await _signInManager
                .CheckPasswordSignInAsync(user, request.Password, false);

            if (!signInResult.Succeeded)
            {
                return Result.Failure(AuthErrors.InvalidLoginCredentials);
            }
            
            var accessToken = _tokenProvider.CreateToken(user);
            var refreshToken =  _tokenProvider.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken()
            {
                Token = refreshToken,
                UserId =  user.Id,
                ExpiresOnUtc = DateTime.UtcNow.AddDays(14)
            };
            
            _context.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync(cancellationToken);
                
            _cookieService.Store("accessToken", accessToken, DateTime.Now.AddMinutes(15));
            _cookieService.Store("refreshToken", refreshToken, DateTime.Now.AddDays(14));

            return Result.Success();
        }
    }
}