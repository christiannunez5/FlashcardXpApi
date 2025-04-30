using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Auth.Payloads;
using Application.Features.StudySets.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Groups.Queries;

public static class GetGroupMembers
{
    public class Query : IRequest<Result<List<UserDto>>>
    {
        public required string GroupId { get; set; }
    }
    
    public class Handler : IRequestHandler<Query, Result<List<UserDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Handler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<List<UserDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var members = await _context
                .Groups
                .Include(g => g.GroupMembers)
                .Select(g => g.GroupMembers)
                .ToListAsync(cancellationToken);

            return Result.Success(_mapper.Map<List<UserDto>>(members));
        }
    }
}