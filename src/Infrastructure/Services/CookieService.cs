using Application.Common.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CookieService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }
        
    public string? Get(string key)
    {
        if (_contextAccessor.HttpContext?.Request.Cookies.TryGetValue(key, out var value) == true)
        {
            return value;
        }
        return null;
    }

    public void Remove(string key)
    {
        _contextAccessor.HttpContext?.Response.Cookies.Delete(key);
    }

    public void Store(string key, string value, DateTime lifeTime)
    {
        _contextAccessor.HttpContext?.Response.Cookies.Append(key, value,
            new CookieOptions
            {
                Expires = lifeTime,
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                IsEssential = true
            }
        );
    }
}