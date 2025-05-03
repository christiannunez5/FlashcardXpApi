using Application.Common.Abstraction;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Folders.Commands;

public static class DeleteFolderById
{
    public class Command : IRequest<Result<string>>
    {
        public required string FolderId { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IApplicationDbContext  _context;
        private readonly IUserContext _userContext;

        public Handler(IApplicationDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }
        
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var folder = await _context
                .Folders
                .Include(f => f.CreatedBy)
                .Include(f => f.SubFolders)
                .FirstOrDefaultAsync(f => f.Id == request.FolderId, cancellationToken);

            if (folder == null)
            {
                return Result.Failure<string>(FolderErrors.FolderNotFound);
            }

            if (folder.CreatedById != _userContext.UserId())
            {
                return Result.Failure<string>(FolderErrors.NotOwner);
            }

            _context.Folders.Remove(folder);
            _context.Folders.RemoveRange(folder.SubFolders);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success(request.FolderId);

        }
    }
}