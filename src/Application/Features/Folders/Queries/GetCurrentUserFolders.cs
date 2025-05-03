using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Folders.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Folders.Queries;

public static class GetCurrentUserFolders
{
    public class Query : IRequest<Result<List<FolderBriefDto>>>
    {
        
    }
    
    public class Handler : IRequestHandler<Query, Result<List<FolderBriefDto>>>
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
        
        public async Task<Result<List<FolderBriefDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var folders = await _context
                .Folders
                .Where(f => f.CreatedById == _userContext.UserId() &&
                            f.ParentFolderId == null)
                .ToListAsync(cancellationToken);

            return Result.Success(_mapper.Map<List<FolderBriefDto>>(folders));

        }
    }
}