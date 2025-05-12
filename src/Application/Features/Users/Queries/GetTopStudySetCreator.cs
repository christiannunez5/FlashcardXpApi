using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Users.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Queries;

public static class GetTopStudySetCreator
{
    public class Query : IRequest<Result<List<TopStudySetCreatorDto>>>
    {
        
    }
    
    public class Handler : IRequestHandler<Query, Result<List<TopStudySetCreatorDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<TopStudySetCreatorDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var topCreators = await _context.StudySets
                .GroupBy(s => s.CreatedBy)
                .Select(g => new
                {
                    User = g.Key,
                    StudySetCount = g.Count()
                })
                .OrderByDescending(x => x.StudySetCount)
                .Take(10)
                .ToListAsync(cancellationToken);
                
            var result = topCreators.Select(x => new TopStudySetCreatorDto
            {
                User = _mapper.Map<UserDto>(x.User),
                StudySetsCreatedCount = x.StudySetCount
            }).ToList();

            return Result.Success(result);
        }
    }
}