using AutoMapper;
using FlashcardXpApi.Auth.Requests;
using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Users;
using FlashcardXpApi.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlashcardXpApi.Auth
{
    public class AuthService
    {

        private readonly IUserRepository _userRepo;
        private readonly ILogger _logger;
        private readonly CreateUserRequestValidator _createUserValidator;
        private readonly IMapper _mapper;
        private readonly TokenProvider _tokenProvider;
        private readonly IPasswordHasher _passwordHasher;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthService(IUserRepository userRepo, 
            ILogger<AuthService> logger, CreateUserRequestValidator createUserValidator, 
            IMapper mapper, TokenProvider tokenProvider, 
            IPasswordHasher passwordHasher,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userRepo = userRepo;
            _logger = logger;
            _createUserValidator = createUserValidator;
            _mapper = mapper;
            _tokenProvider = tokenProvider;
            _passwordHasher = passwordHasher;
            _userManager = userManager;
            _signInManager = signInManager;
        }
   

        public async Task<Result> Login(UserLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return Result.Failure(AuthErrors.InvalidLoginRequest);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                return Result.Failure(AuthErrors.UserNotFoundError);
            }

            return Result.Success;
        }

        public async Task<ResultGeneric<IdentityResult>> Register(CreateUserRequest request)
        {
            var validationResult = await _createUserValidator.ValidateAsync(request);
            
            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors
                    .Select(x => x.ErrorMessage)
                    .First();

                return ResultGeneric<IdentityResult>.Failure(AuthErrors.CreateUserRequestError(errorMessage));
            }

            bool IsEmailUnique = await _userRepo.IsEmailUnique(request.Email);

            if (!IsEmailUnique)
            {
                _logger.LogInformation($"The email {request.Email} is not unique.");
                return ResultGeneric<IdentityResult>.Failure(AuthErrors.EmailMustBeUnique);
            }

            var newUser = new User
            { 
                Email = request.Email,
                UserName = request.Username,
               ProfilePicUrl = request.ProfilePicUrl,
            };  

            var createdUser = await _userManager.CreateAsync(newUser, request.Password);

            return ResultGeneric<IdentityResult>.Success(createdUser);
        }

             
    }
}
