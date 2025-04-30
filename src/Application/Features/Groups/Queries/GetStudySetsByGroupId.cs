using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.StudySets.Payloads;
using AutoMapper;
using Domain.Entities.Studysets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Groups.Queries;


public static class GetStudySetsByGroupId
{
    public class Query : IRequest<Result<List<StudySetBriefDto>>>
    {
        public required string GroupId { get; set; }
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

            var group = await _context
                .Groups
                .FirstOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);

            if (group == null)
            {
                return Result.Failure<List<StudySetBriefDto>>(GroupErrors.GroupNotFound);
            }
            
            var studysets = await _context
                .GroupStudySets
                .Include(gs => gs.StudySet)
                .Where(gs => gs.GroupId == request.GroupId)
                .Select(gs => gs.StudySet)
                .ToListAsync(cancellationToken);

            return Result.Success(_mapper.Map<List<StudySetBriefDto>>(studysets));
        }
    }
}
