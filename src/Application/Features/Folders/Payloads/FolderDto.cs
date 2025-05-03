using AutoMapper;
using Domain.Entities.Folders;

namespace Application.Features.Folders.Payloads;

public class FolderDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public List<FolderBriefDto> SubFolders { get; init; } = new();

    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Folder, FolderDto>();
        }
    }
}