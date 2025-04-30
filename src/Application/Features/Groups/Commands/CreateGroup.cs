using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Groups.Payloads;
using Application.Features.StudySets.Payloads;
using AutoMapper;
using Domain.Entities.Groups;
using MediatR;

namespace Application.Features.Groups.Commands;

public static class CreateGroup
{
    public class Command : IRequest<Result<GroupBriefDto>>
    {
        public required string Name { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<GroupBriefDto>>
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

        public async Task<Result<GroupBriefDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var newGroup = new Group
            {
                Name = request.Name,
                CreatedById = _userContext.UserId()
            };
            
            _context.Groups.Add(newGroup);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(_mapper.Map<GroupBriefDto>(newGroup));
        }
    }
}