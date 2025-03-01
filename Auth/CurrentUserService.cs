using FlashcardXpApi.Auth.Interfaces;
using FlashcardXpApi.Users;
using Microsoft.AspNetCore.Identity;

namespace FlashcardXpApi.Auth
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
           
            if (_contextAccessor.HttpContext != null)
            {
                var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
                return user;
            }

            return null;
        }
    }
}
