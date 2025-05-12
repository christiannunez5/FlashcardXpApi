using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Tags.Payloads;
using AutoMapper;
using Domain.Entities.Tags;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Queries;

public static class GetStudySetTagsById
{
    public class Query : IRequest<Result<List<TagDto>>>
    {
        public required string StudySetId { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Result<List<TagDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public Handler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<TagDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var studyset = await _context
                .StudySets
                .FirstOrDefaultAsync(s => s.Id == request.StudySetId, cancellationToken);

            if (studyset == null)
            {
                return Result.Failure<List<TagDto>>(StudySetErrors.StudySetNotFound);
            }

            var tags = await _context
                .StudySetTags
                .Where(st => st.StudySetId == request.StudySetId)
                .Select(s => s.Tag)
                .ToListAsync(cancellationToken);

            return Result.Success(_mapper.Map<List<TagDto>>(tags));

        }
    }
}