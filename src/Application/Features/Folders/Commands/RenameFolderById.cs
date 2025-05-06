using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Folders.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Folders.Commands;

public static class RenameFolderById
{
    public class Command : IRequest<Result<FolderBriefDto>>
    {
        public required string FolderId { get; set; }
        public required string Name { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<FolderBriefDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;

        public Handler(IMapper mapper, IApplicationDbContext context, IUserContext userContext)
        {
            _mapper = mapper;
            _context = context;
            _userContext = userContext;
        }

        public async Task<Result<FolderBriefDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var folder = await _context
                .Folders
                .FirstOrDefaultAsync(f => f.Id == request.FolderId, cancellationToken);

            if (folder == null)
            {
                return Result.Failure<FolderBriefDto>(FolderErrors.FolderNotFound);
            }

            if (folder.CreatedById != _userContext.UserId())
            {
                return Result.Failure<FolderBriefDto>(FolderErrors.NotOwner);
            }
            
            folder.Name = request.Name;
                
            _context.Folders.Update(folder);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(_mapper.Map<FolderBriefDto>(folder));

        }
    }
}