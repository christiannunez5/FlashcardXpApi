
namespace Application.Features.Flashcards.Payloads;

public record CreateFlashcardRequest(string StudySetId, string Term, string Definition);

