

using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.UserQuests.Payloads;
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
        private readonly IDateTimeProvider _dateTimeProvider;
        public Handler(IApplicationDbContext context, IUserContext userContext, IMapper mapper, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _userContext = userContext;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<List<UserQuestDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userQuests = await _context
                .UserQuests
                .Include(uq => uq.Quest)
                .Where(uq => uq.UserId == _userContext.UserId() && uq.IsCompleted == false)
                .OrderBy(uq => uq.Quest.Goal)
                .ToListAsync(cancellationToken);

            foreach (var userQuest in userQuests)
            {
                var flashcardsCompleted = await _context
                    .CompletedFlashcards
                    .Where(fc => fc.UserId == _userContext.UserId() && 
                                                    fc.Date == DateOnly.FromDateTime(_dateTimeProvider.Today()))
                    .CountAsync(cancellationToken);
                    
                if (flashcardsCompleted > userQuest.Quest.Goal)
                {
                    flashcardsCompleted = userQuest.Quest.Goal;
                }
                
                userQuest.CompletedFlashcards = flashcardsCompleted;
            }
            
            return Result.Success(_mapper.Map<List<UserQuestDto>>(userQuests));
        }
    }
}
