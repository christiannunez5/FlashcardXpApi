using AutoMapper;
using Domain.Entities.Tags;

namespace Application.Features.Tags.Payloads;

public class TagDto
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Tag, TagDto>();
        }
    }
}