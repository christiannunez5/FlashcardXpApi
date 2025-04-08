using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Domain;
using Microsoft.AspNetCore.Identity;

namespace FlashcardXpApi.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {

        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserService(UserManager<User> userManager,
            IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<User?> GetCurrentUser()
        {
            
            var httpContext = _contextAccessor.HttpContext;

            if (httpContext?.User.Identity?.IsAuthenticated != true)
            {
                return null;
            }
                
            var user = await _userManager.GetUserAsync(httpContext.User);

            return user;

        }
    }
}
