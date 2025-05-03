using AutoMapper;
using Domain.Entities.Folders;

namespace Application.Features.Folders.Payloads;

public class FolderBriefDto
{
    public required string Id { get; init; }
    
    public required string Name { get; init; }
    
    public string? ParentFolderId { get; init; }
    
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Folder, FolderBriefDto>();
        }
    }
}