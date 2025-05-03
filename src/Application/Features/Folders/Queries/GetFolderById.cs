using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Folders.Payloads;
using AutoMapper;
using Domain.Entities.Folders;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Folders.Queries;

public static class GetFolderById
{
    public class Query : IRequest<Result<FolderDto>>
    {
        public required string FolderId { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Result<FolderDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Handler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        
        public async Task<Result<FolderDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var folder = await _context
                .Folders
                .Include(f => f.SubFolders)
                .FirstOrDefaultAsync(f => f.Id == request.FolderId, cancellationToken);
            
            if (folder is null)
            {
                return Result.Failure<FolderDto>(FolderErrors.FolderNotFound);
            }
            
            return Result.Success(_mapper.Map<FolderDto>(folder));
        }
    }
}