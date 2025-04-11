using AutoMapper;
using Domain.Entities.Flashcards;

namespace Application.Features.Flashcards.Payloads;

public record FlashcardDto(string Id, string Term, string Definition)
{
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Flashcard, FlashcardDto>();
        }
    }
}