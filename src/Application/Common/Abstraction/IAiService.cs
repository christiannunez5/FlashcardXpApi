using Domain.Entities.Flashcards;

namespace Application.Common.Abstraction;

public interface IAiService
{
    Task<List<Flashcard>> GenerateFlashcardFromText(string content, CancellationToken cancellationToken);
    Task<List<Flashcard>> GenerateFlashcardFromTopic(string topic, CancellationToken cancellationToken);
}