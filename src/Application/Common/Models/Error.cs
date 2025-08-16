namespace Application.Common.Models;

public record Error(string Code, string Message)
{
    public static Error None => new(ErrorTypeConstant.NONE, String.Empty);
}

public class ErrorTypeConstant
{
    public const string NONE = "";
    public const string NOT_FOUND = "Not found error";
    public const string VALIDATION_ERROR = "Validation error";
    public const string AUTHENTICATION_ERROR = "Authentication required error"; // ✅ Fixed
    public const string AUTHORIZATION_ERROR = "Access denied error";           
    public const string INTERNAL_SERER_ERROR = "Internal server error";
    public const string BAD_REQUEST = "Bad request error";
    public const string FORBIDDEN = "Forbidden error";
    public const string CONFLICT = "Conflict error";
}