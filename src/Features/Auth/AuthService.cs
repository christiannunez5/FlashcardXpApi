using AutoMapper;
using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Features.Auth.Requests;
using FlashcardXpApi.Features.Users;
using Microsoft.AspNetCore.Identity;


namespace FlashcardXpApi.Features.Auth
{
    public class AuthService : IAuthService
    {
        private readonly CreateUserRequestValidator _createUserValidator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtHandler _jwtHandler;
        private readonly ITokenService _tokenService;

        public AuthService(CreateUserRequestValidator createUserValidator,
                           UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           JwtHandler jwtHandler,
                           ITokenService tokenService)
        {
            _createUserValidator = createUserValidator;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtHandler = jwtHandler;
            _tokenService = tokenService;
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

            var accessToken = _jwtHandler.CreateToken(user);
            _tokenService.StoreToken(accessToken);

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



    }
}
