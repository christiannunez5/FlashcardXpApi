using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Features.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace FlashcardXpApi.Features.Auth
{
    public class AuthService : IAuthService
    {
        private readonly CreateUserRequestValidator _createUserValidator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtHandler _jwtHandler;
        private readonly ICookieService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public AuthService(CreateUserRequestValidator createUserValidator,
                           UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           JwtHandler jwtHandler,
                           ICookieService tokenService,
                           IRefreshTokenRepository refreshTokenRepository)
        {
            _createUserValidator = createUserValidator;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtHandler = jwtHandler;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }


        public async Task<Result> Login(UserLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return Result.Failure(AuthErrors.UserNotFoundError);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                return Result.Failure(AuthErrors.InvalidLoginRequest);
            }

            var refreshToken = new RefreshToken
            {
                Token = _jwtHandler.GenerateRefreshToken(),
                User = user,
                ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.InsertAsync(refreshToken);

            var accessToken = _jwtHandler.CreateToken(user);


            _tokenService.Store("accessToken", accessToken, DateTime.UtcNow.AddMinutes(15));
            _tokenService.Store("refreshToken", refreshToken.Token, DateTime.UtcNow.AddDays(14));

            return Result.Success;
        }

        public async Task<Result> LoginWithRefreshToken()
        {
            var tokenEntry = _tokenService.Get("refreshToken");

            if (tokenEntry is null)
            {
                return Result.Failure(AuthErrors.AuthorizationFailedError);
            }

            var refreshToken = await _refreshTokenRepository.GetByToken(tokenEntry);

            if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.Now)
            {
                return Result.Failure(AuthErrors.AuthorizationFailedError);
            }

            var accessToken = _jwtHandler.CreateToken(refreshToken.User);
            refreshToken.Token = _jwtHandler.GenerateRefreshToken();
            refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);

            await _refreshTokenRepository.UpdateAsync(refreshToken);

            _tokenService.Store("accessToken", accessToken, DateTime.UtcNow.AddMinutes(15));
            _tokenService.Store("refreshToken", refreshToken.Token, DateTime.UtcNow.AddDays(14));

            return Result.Success;
        }

        public async Task<Result> Register(CreateUserRequest request)
        {

            var validationResult = _createUserValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors
                    .Select(x => x.ErrorMessage)
                    .First();

                return Result.Failure(AuthErrors.CreateUserValidationError(errorMessage));
            }

            var newUser = new User
            {
                Email = request.Email,
                UserName = request.Username,
                ProfilePicUrl = request.ProfilePicUrl,
            };

            var createdUser = await _userManager.CreateAsync(newUser, request.Password);

            if (!createdUser.Succeeded)
            {
                var errorMessage = createdUser.Errors.First().Description;
                return Result.Failure(
                    AuthErrors.CreateUserValidationError(errorMessage));
            }

            return Result.Success;
        }
        
        public async Task<Result> Logout()
        {

            var tokenEntry = _tokenService.Get("refreshToken");

            if (tokenEntry == null)
            {
                throw new Exception("No refresh token found.");
            }

            var refreshToken = await _refreshTokenRepository.GetByToken(tokenEntry);

            if (refreshToken != null)
            {
                await _refreshTokenRepository.DeleteAsync(refreshToken);
            }

            _tokenService.Remove("accessToken");
            _tokenService.Remove("refreshToken");

            return Result.Success;
        }
    }
}
