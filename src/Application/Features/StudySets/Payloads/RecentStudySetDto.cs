using AutoMapper;
using Domain.Entities.Studysets;

namespace Application.Features.StudySets.Payloads;

public record RecentStudySetDto(string Id, string Title, DateTime AccessedAt)
{
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<StudySet, RecentStudySetDto>();
        }
    }
}