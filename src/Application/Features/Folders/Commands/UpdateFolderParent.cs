using Application.Common.Abstraction;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Folders.Commands;

public static class UpdateFolderParentById
{
    public class Command : IRequest<Result>
    {
        public required string FolderId { get; set; }
        public required string ParentFolderId { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result>
    {

        private readonly IApplicationDbContext _context;

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var folder = await _context
                .Folders
                .FirstOrDefaultAsync(f => f.Id == request.FolderId, cancellationToken);
            
            var parentFolder = await _context
                .Folders
                .FirstOrDefaultAsync(f => f.Id == request.ParentFolderId, cancellationToken);

            if (parentFolder == null || folder == null)
            {
                return Result.Failure(FolderErrors.FolderNotFound);
            }
            
            folder.ParentFolderId = parentFolder.Id;
            
            _context.Folders.Update(folder);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}