using AutoMapper;
using FlashcardXpApi.Auth.Requests;
using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Users;
using FlashcardXpApi.Validations;
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
        private readonly IConfiguration _config;
        private readonly TokenProvider _tokenProvider;

        public AuthService(IUserRepository userRepo, 
            ILogger<AuthService> logger, CreateUserRequestValidator createUserValidator, 
            IMapper mapper, IConfiguration config, TokenProvider tokenProvider)
        {
            _userRepo = userRepo;
            _logger = logger;
            _createUserValidator = createUserValidator;
            _mapper = mapper;
            _config = config;
            _tokenProvider = tokenProvider;
        }

        public string Login(UserLoginRequest request)
        {
            var user = AuthenticateUser(request);

            return _tokenProvider.Create(user);
        }

        private User AuthenticateUser(UserLoginRequest request)
        {
            var user = _mapper.Map<User>(request);
            user.Id = 29;
            return _mapper.Map<User>(request);
        }

        public async Task<Result> Register(CreateUserRequest request)
        {
            var validationResult = await _createUserValidator.ValidateAsync(request);
            
            if (!validationResult.IsValid)
            {
                var errorMessage = validationResult.Errors
                    .Select(x => x.ErrorMessage)
                    .First();

                return Result.Failure(AuthErrors.CreateUserRequestError(errorMessage));
            }

            bool IsEmailUnique = await _userRepo.IsEmailUnique(request.Email);

            if (!IsEmailUnique)
            {
                _logger.LogInformation($"The email {request.Email} is not unique.");
                return Result.Failure(AuthErrors.EmailMustBeUnique);
            }

            var newUser = _mapper.Map<User>(request);
                
            _userRepo.Insert(newUser);
            _userRepo.SaveChangesAsync();

            return Result.Success;
        }

             
    }
}
