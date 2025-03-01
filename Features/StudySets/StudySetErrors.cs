using FlashcardXpApi.Common.Results;

namespace FlashcardXpApi.Features.StudySets
{
    public class StudySetErrors
    {


        public static Error StudySetNotFoundError =>
            new Error(ErrorTypeConstant.NOT_FOUND, "Study set not found.");

        public static Error UserNotFoundError =>
            new Error(ErrorTypeConstant.NOT_FOUND, "User not found.");

        public static Error UserIsAlreadyParticipant =>
            new Error(ErrorTypeConstant.BAD_REQUEST, "User is already participant.");

        public static Error AuthenticationRequiredError =>
            new Error(ErrorTypeConstant.UNAUTHORIZED, "Authenticated required.");

        public static Error AuthorizationFailedError =>
           new Error(ErrorTypeConstant.FORBIDDEN, "Not authorized to perform this action.");

        public static Error ValidationError(string message) =>
           new Error(ErrorTypeConstant.VALIDATION_ERROR, message);
    }
}
