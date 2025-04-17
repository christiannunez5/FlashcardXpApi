

using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Quests.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Quests.Queries;

public static class GetCurrentUserQuests
{
    public class Query : IRequest<Result<List<UserQuestDto>>>
    {

    }

    public class Handler : IRequestHandler<Query, Result<List<UserQuestDto>>>
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

        public async Task<Result<List<UserQuestDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userQuests = await _context
                .UserQuests
                .Include(uq => uq.Quest)
                .Where(uq => uq.UserId == _userContext.UserId())
                .ToListAsync(cancellationToken);

            return Result.Success(_mapper.Map<List<UserQuestDto>>(userQuests));
        }
    }
}
