using Application.Common.Abstraction;
using Application.Common.Models;
using Domain.Entities.Folders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Folders.Commands;

public static class CreateFolder
{
    public class Command : IRequest<Result<string>>
    {
        public string? ParentFolderId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    
    public class Handler : IRequestHandler<Command, Result<string>>
    {
        
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;
        
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {

            if (request.ParentFolderId != null)
            {
                var parentFolder = await _context
                    .Folders
                    .FirstOrDefaultAsync(f => f.Id == request.ParentFolderId, cancellationToken);

                if (parentFolder == null)
                {
                    return Result.Failure<string>(FolderErrors.FolderNotFound);
                }
            }
            
            var newFolder = new Folder
            {
                CreatedById = _userContext.UserId(),
                Name = request.Name,
                ParentFolderId = request.ParentFolderId
            };
            
            _context.Folders.Add(newFolder);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(newFolder.Id);

        }
    }
}