using Application.Common.Abstraction;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication;

public class UserContext : IUserContext
{
    private  readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<User> _userManager;
    
    public UserContext(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }
    
    public string UserId() =>
        _httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");
}