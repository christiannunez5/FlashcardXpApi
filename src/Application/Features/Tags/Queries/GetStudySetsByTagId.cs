using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.StudySets.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tags.Queries;

public static class GetStudySetsByTagId
{
    public class Query : IRequest<Result<List<StudySetBriefDto>>>
    {
        public required string  TagId { get; set; }
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
            var studysets = await _context
                .StudySets
                .Include(s => s.CreatedBy)
                .Where(s => s.StudySetTags.Any(st => st.TagId == request.TagId))
                .ToListAsync(cancellationToken);
            
            return Result.Success(_mapper.Map<List<StudySetBriefDto>>(studysets));
        }
    }
}