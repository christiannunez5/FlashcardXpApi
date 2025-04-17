

namespace Application.Features.Flashcards.Payloads;

public record UpdateFlashcardRequest(string? Id, 
    string Term, string Definition, string StudySetId);