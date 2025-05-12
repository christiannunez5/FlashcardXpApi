using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Tags.Payloads;
using AutoMapper;
using Domain.Entities.Tags;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tags.Queries;

public static class GetTags
{
    public class Query : IRequest<Result<List<TagDto>>>
    {

    };
    
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
            var tags = await _context
                .Tags
                .ToListAsync(cancellationToken);
            
            return Result.Success(_mapper.Map<List<TagDto>>(tags));
        }
    }
}