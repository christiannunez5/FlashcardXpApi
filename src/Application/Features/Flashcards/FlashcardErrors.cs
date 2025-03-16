using FlashcardXpApi.Application.Common;

namespace FlashcardXpApi.Application.Features.Flashcards
{
    public static class FlashcardErrors
    {
        public static Error FlashcardNotFoundError =
            new Error(ErrorTypeConstant.NOT_FOUND, "Flashcard not found.");

        public static Error ValidationError (string message) =>
            new Error(ErrorTypeConstant.VALIDATION_ERROR, message);
    }
}
