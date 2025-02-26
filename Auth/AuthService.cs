using AutoMapper;
using FlashcardXpApi.Auth.Requests;
using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Users;
using Microsoft.AspNetCore.Identity;


namespace FlashcardXpApi.Auth
{
    public class AuthService
    {
        private readonly CreateUserRequestValidator _createUserValidator;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JwtHandler _jwtHandler;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(CreateUserRequestValidator createUserValidator,
                           IMapper mapper,
                           UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           JwtHandler jwtHandler,
                           IHttpContextAccessor contextAccessor)
        {
            _createUserValidator = createUserValidator;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtHandler = jwtHandler;
            _contextAccessor = contextAccessor;
        }

        public async Task<Result> Login(UserLoginRequest request, HttpContext context)
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

            SetTokenInsideCookie(accessToken, context);

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

        private void SetTokenInsideCookie(string token, HttpContext context)
        {
            context.Response.Cookies.Append("accessToken", token, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(30),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true,
            });
        }

        public async Task<ResultGeneric<UserDto>> GetLoggedInUserHttp()
        {
            var loggedInUser = await GetLoggedInUser();
            var user = _mapper.Map<UserDto>(loggedInUser);
            return ResultGeneric<UserDto>.Success(user);
        }

        public async Task<User?> GetLoggedInUser()
        {
            if (_contextAccessor.HttpContext != null)
            {
                var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                return user;
            }

            return null;
            
        }
         
    }
}
