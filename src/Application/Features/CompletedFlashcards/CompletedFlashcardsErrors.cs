
using Application.Common.Models;

namespace Application.Features.CompletedFlashcards;

public class CompletedFlashcardsErrors
{
    public static Error CannotMarkCompleteAgain =
        new Error(ErrorTypeConstant.BAD_REQUEST, "This flashcard has already been marked as completed.");
}
