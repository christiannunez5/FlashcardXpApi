using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.StudySets.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Queries;

public static class GetPopularStudySets
{
    public class Query : IRequest<Result<List<StudySetBriefDto>>>
    {
        
    }
    
    public class Handler : IRequestHandler<Query, Result<List<StudySetBriefDto>>>
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

        public async Task<Result<List<StudySetBriefDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var popularStudySets = await _context
                .StudySets
                .Include(s => s.StudySetRecords)
                .Include(s => s.CreatedBy)
                .OrderBy(s => s.StudySetRecords.Count)
                .Take(10)
                .ToListAsync(cancellationToken);

            return Result.Success(_mapper.Map<List<StudySetBriefDto>>(popularStudySets));


        }
    }
}