using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Folders.Payloads;
using AutoMapper;
using Domain.Entities.Folders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Folders.Commands;

public static class CreateFolder
{
    public class Command : IRequest<Result<FolderBriefDto>>
    {
        public string? FolderId { get; init; }
        public string Name { get; init; } = string.Empty;
    }
    
    public class Handler : IRequestHandler<Command, Result<FolderBriefDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        
        public Handler(IApplicationDbContext context, IUserContext userContext, IMapper mapper)
        {
            _context = context;
            _userContext = userContext;
            _mapper = mapper;
        }
        
        public async Task<Result<FolderBriefDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.FolderId != null)
            {
                var parentFolder = await _context
                    .Folders
                    .FirstOrDefaultAsync(f => f.Id == request.FolderId, cancellationToken);

                if (parentFolder == null)
                {
                    return Result.Failure<FolderBriefDto>(FolderErrors.FolderNotFound);
                }
            }
            
            var newFolder = new Folder
            {
                CreatedById = _userContext.UserId(),
                Name = request.Name,
                ParentFolderId = request.FolderId
            };
            
            _context.Folders.Add(newFolder);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success(_mapper.Map<FolderBriefDto>(newFolder));

        }
    }
}