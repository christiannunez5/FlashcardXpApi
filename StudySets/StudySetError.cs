using FlashcardXpApi.Common.Results;

namespace FlashcardXpApi.FlashcardSets
{
    public class StudySetError
    {
        public static Error UserNotFoundError =>
            new Error(ErrorTypeConstant.NOT_FOUND, "User not found.");

        public static Error StudySetNotFoundError =>
            new Error(ErrorTypeConstant.NOT_FOUND, "Study set not found.");

        public static Error StudySetAccessDeniedError =>
            new Error(ErrorTypeConstant.UNAUTHORIZED, "Unauthorized access to the study set.");

        public static Error ValidationError (string message) =>
           new Error(ErrorTypeConstant.VALIDATION_ERROR, message);
    }
}
