using FlashcardXpApi.Common.Results;


namespace FlashcardXpApi.Auth
{
    public class AuthErrors
    {
        public static Error InvalidLoginRequest =>
            new Error(ErrorTypeConstant.BAD_REQUEST, "Invalid Login Request.");

        public static Error EmailMustBeUnique =>
            new Error(ErrorTypeConstant.CONFLICT, "Email already exists.");

        public static Error CreateUserRequestError (string message) =>
            new Error(ErrorTypeConstant.VALIDATION_ERROR, message);
    }
}
