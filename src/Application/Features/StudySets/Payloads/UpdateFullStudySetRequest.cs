using Application.Features.Flashcards.Payloads;

namespace Application.Features.StudySets.Payloads;

public record UpdateFullStudySetRequest(string Title, string Description, List<UpdateFlashcardRequest> Flashcards);    
