using Application.Common.Models;

namespace Application.Features.Auth;

public class AuthErrors
{
    public static Error EmailAlreadyTaken =
        new Error(ErrorTypeConstant.CONFLICT, "Email already taken");
    
    public static Error InvalidLoginCredentials =
        new Error(ErrorTypeConstant.BAD_REQUEST, "Invalid username or password");
    
    public static Error NotAuthorize = 
        new Error(ErrorTypeConstant.FORBIDDEN, "Not authorized to perform this action");
    
    public static Error ValidationError (string message) =>
        new Error(ErrorTypeConstant.VALIDATION_ERROR, message);
}