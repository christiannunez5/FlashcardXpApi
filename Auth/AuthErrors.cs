using FlashcardXpApi.Common.Results;


namespace FlashcardXpApi.Auth
{
    public class AuthErrors
    {
        public static Error InvalidLoginRequest =>
            new Error(ErrorTypeConstant.BAD_REQUEST, "Invalid email or password.");

        public static Error EmailMustBeUnique =>
            new Error(ErrorTypeConstant.CONFLICT, "Email already exists.");

        public static Error UserNotFoundError =>
            new Error(ErrorTypeConstant.NOT_FOUND, "User does not exist.");
   

        public static Error CreateUserValidationError(string message) =>
      new Error(ErrorTypeConstant.BAD_REQUEST, message);
    }
}
