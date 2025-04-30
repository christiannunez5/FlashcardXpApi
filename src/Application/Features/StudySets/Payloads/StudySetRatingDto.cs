using AutoMapper;
using Domain.Entities.Studysets;

namespace Application.Features.StudySets.Payloads;

public class StudySetRatingDto
{
    public double AverageRating { get; init; }
    public int RatedByCount { get; init; }
    
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StudySet, StudySetRatingDto>()
                .ForMember(dest => dest.AverageRating, opts => opts.MapFrom(src => src.AverageRating()))
                .ForMember(dest => dest.RatedByCount, opts => opts.MapFrom(src => src.StudySetRatings.Count()));

        }
    }
}