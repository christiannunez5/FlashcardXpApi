using Application.Common.Abstraction;
using Application.Common.Models;
using Domain.Entities.Folders;
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
                .Include(f => f.SubFolders)
                .Include(f => f.StudySets)
                .FirstOrDefaultAsync(f => f.Id == request.FolderId, cancellationToken);
            
            if (folder == null)
            {
                return Result.Failure<string>(FolderErrors.FolderNotFound);
            }
            
            if (folder.CreatedById != _userContext.UserId())
            {
                return Result.Failure<string>(FolderErrors.NotOwner);
            }
            
            await DeleteFolderRecursively(folder.Id, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(request.FolderId);

        }
        
        private async Task DeleteFolderRecursively(string folderId, CancellationToken cancellationToken)
        {
            var folder = await _context.Folders
                .Include(f => f.SubFolders)
                .Include(f => f.StudySets)
                    .ThenInclude(s => s.Flashcards)
                .Include(f => f.StudySets)
                    .ThenInclude(s => s.StudySetRatings)
                .Include(f => f.StudySets)
                    .ThenInclude(s => s.StudySetRecords)
                .Include(f => f.StudySets)
                    .ThenInclude(s => s.RecentStudySets)
                .Include(s => s.StudySets)
                    .ThenInclude(s => s.StudySetTags)
                .AsSingleQuery()
                .FirstAsync(f => f.Id == folderId, cancellationToken);
            
            foreach (var subFolder in folder.SubFolders)
            { 
                await DeleteFolderRecursively(subFolder.Id, cancellationToken);
            }
            
            foreach (var studySet in folder.StudySets)
            {
                _context.RecentStudySets.RemoveRange(studySet.RecentStudySets);
                _context.Flashcards.RemoveRange(studySet.Flashcards);
                _context.StudySetRecords.RemoveRange(studySet.StudySetRecords);
                _context.StudySetRatings.RemoveRange(studySet.StudySetRatings);
                _context.StudySetTags.RemoveRange(studySet.StudySetTags);
                _context.StudySets.Remove(studySet);
            }
            
            _context.StudySets.RemoveRange(folder.StudySets);
            _context.Folders.Remove(folder);
        }
    }
}