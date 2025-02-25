using AutoMapper;
using FlashcardXpApi.Auth.Requests;
using FlashcardXpApi.Common.Results;
using FlashcardXpApi.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlashcardXpApi.Auth
{
    public class AuthService
    {

        private readonly CreateUserRequestValidator _createUserValidator;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TokenProvider _tokenProvider;
        private readonly JwtHandler _jwtHandler;

        public AuthService(CreateUserRequestValidator createUserValidator,
                           IMapper mapper,
                           UserManager<User> userManager,
                           SignInManager<User> signInManager,
                           TokenProvider tokenProvider,
                           JwtHandler jwtHandler)
        {
            _createUserValidator = createUserValidator;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenProvider = tokenProvider;
            _jwtHandler = jwtHandler;
        }

        public async Task<ResultGeneric<string>> Login(UserLoginRequest request, HttpContext context)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return ResultGeneric<string>.Failure(AuthErrors.UserNotFoundError);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                return ResultGeneric<string>.Failure(AuthErrors.InvalidLoginRequest);
            }
            
            var accessToken = _jwtHandler.CreateToken(user);

            SetTokenInsideCookie(accessToken, context);

            return ResultGeneric<string>.Success(accessToken);
        }

        public async Task<ResultGeneric<string>> Register(CreateUserRequest request)
        {

            var validationResult = _createUserValidator.Validate(request);

            if (!validationResult.IsValid)
            {

                var errorMessage = validationResult.Errors
                    .Select(x => x.ErrorMessage)
                    .First();

                return ResultGeneric<string>.Failure(AuthErrors.CreateUserValidationError(errorMessage));
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
                return ResultGeneric<string>.Failure(
                    AuthErrors.CreateUserValidationError(errorMessage));
            }

            return ResultGeneric<string>.Success("Registered successfully.");
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
         
    }
}
