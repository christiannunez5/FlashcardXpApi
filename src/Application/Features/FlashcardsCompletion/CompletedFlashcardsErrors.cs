using FlashcardXpApi.Application.Common;

namespace FlashcardXpApi.Application.Features.FlashcardsCompletion;

public class CompletedFlashcardsErrors
{
    public static Error CannotMarkCompletedAgain =
        new Error(ErrorTypeConstant.CONFLICT, "This flashcard has already been marked as completed.");
}