using Application.Common.Models;

namespace Application.Features.Flashcards;

public class FlashcardErrors
{
    public static Error FlashcardNotFound = 
        new Error(ErrorTypeConstant.NOT_FOUND, "Flashcard not found.");
}