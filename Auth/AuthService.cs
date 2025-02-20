using AutoMapper;
using FlashcardXpApi.Auth.Requests;
using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Users;
using FlashcardXpApi.Validations;

namespace FlashcardXpApi.Auth
{
    public class AuthService
    {

        private readonly IUserRepository _userRepo;
        private readonly ILogger _logger;
        private readonly CreateUserRequestValidator _createUserValidator;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepo, 
            ILogger<AuthService> logger, 
            CreateUserRequestValidator createUserValidator,
            IMapper mapper)
        {
            _userRepo = userRepo;
            _logger = logger;
            _createUserValidator = createUserValidator;
            _mapper = mapper;
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

            var newUser = User.Create(
                    request.Email,
                    request.Username,
                    request.Password,
                    request.ProfilePicUrl
                );
                
            _userRepo.Insert(newUser);
            _userRepo.SaveChangesAsync();

            return Result.Success;
        }

       
    }
}
