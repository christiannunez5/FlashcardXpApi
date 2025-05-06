using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Folders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands;

public static class UpdateStudySetFolderById
{
    public class Command : IRequest<Result>
    {
        public required string StudySetId { get; init; }
        public required string FolderId { get; init; }
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
            var studySet = await _context
                .StudySets
                .FirstOrDefaultAsync(f => f.Id == request.StudySetId, cancellationToken);

            if (studySet == null)
            {
                return Result.Failure(StudySetErrors.StudySetNotFound);
            }
            
            var parentFolder = await _context
                .Folders
                .FirstOrDefaultAsync(f => f.Id == request.FolderId, cancellationToken);

            if (parentFolder == null)
            {
                return Result.Failure(FolderErrors.FolderNotFound);
            }
            
            studySet.FolderId = parentFolder.Id;
            
            _context.StudySets.Update(studySet);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}