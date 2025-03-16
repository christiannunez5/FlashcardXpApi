using FlashcardXpApi.Application.Common;

namespace FlashcardXpApi.Application.Features.Auth
{
    public class AuthErrors
    {
        public static Error EmailTaken => new Error(
            ErrorTypeConstant.CONFLICT, "Email already taken.");

        public static Error UserNotFoundError = new Error(
            ErrorTypeConstant.NOT_FOUND, "User not found.");

        public static Error InvalidLoginCredentials = 
            new Error(ErrorTypeConstant.BAD_REQUEST, "Invalid email or password.");
        
        public static Error AuthenticationRequiredError =
            new Error(ErrorTypeConstant.AUTHENTICATION_ERROR, "Authentication required.");

        public static Error AuthorizationFailedError =
            new Error(ErrorTypeConstant.FORBIDDEN, "Unauthorized to perform this action.");


        public static Error ValidationError(string message) =>
            new Error(ErrorTypeConstant.VALIDATION_ERROR, message);
    }
}
