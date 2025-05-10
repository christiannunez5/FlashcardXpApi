using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.StudySets.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Folders.Queries;

public static class GetStudySetsByFolder
{
    public class Query : IRequest<Result<List<StudySetBriefDto>>>
    {
        public required string FolderId { get; init; }
    }
    
    public class Handler : IRequestHandler<Query, Result<List<StudySetBriefDto>>>
    {
        
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Result<List<StudySetBriefDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var folder = await _context
                .Folders
                .Include(f => f.StudySets)
                .ThenInclude(s => s.Flashcards)
                .FirstOrDefaultAsync(f => f.Id == request.FolderId, cancellationToken);
                
            if (folder == null)
            {
                return Result.Failure<List<StudySetBriefDto>>(FolderErrors.FolderNotFound);
            }

            return Result.Success(_mapper.Map<List<StudySetBriefDto>>(folder.StudySets));   
        }
    }
}