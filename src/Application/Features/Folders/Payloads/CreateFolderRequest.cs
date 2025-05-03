namespace Application.Features.Folders.Payloads;

public record CreateFolderRequest(string Name, string? FolderId);