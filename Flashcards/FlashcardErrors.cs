using FlashcardXpApi.Common.Results;

namespace FlashcardXpApi.Flashcards
{
    public class FlashcardErrors
    {
        public static Error StudySetNotFoundError =>
            new Error(ErrorTypeConstant.NOT_FOUND, "Study set not found.");

        public static Error FlashcardNotFoundError =>
           new Error(ErrorTypeConstant.NOT_FOUND, "Flashcard not found.");

        public static Error FlashcardValidationError (string error) =>
            new Error(ErrorTypeConstant.VALIDATION_ERROR, error);
    }
}
