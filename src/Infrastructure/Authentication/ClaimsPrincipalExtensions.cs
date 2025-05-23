using System.Security.Claims;

namespace Infrastructure.Authentication;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId ?? throw new ApplicationException("User id is unavailable.");
    }
}