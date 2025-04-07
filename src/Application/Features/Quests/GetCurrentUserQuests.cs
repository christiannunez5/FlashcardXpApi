using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Contracts;
using FlashcardXpApi.Application.Contracts.Auth;
using FlashcardXpApi.Application.Features.Auth;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.Quests
{
    public static class GetCurrentUserQuests
    {
        public class Query : IRequest<Result<List<UserQuestResponse>>>
        {
           
        };

        public class Handler : IRequestHandler<Query, Result<List<UserQuestResponse>>>
        {

            private readonly ICurrentUserService _currentUserService;
            private readonly DataContext _context;

            public Handler(ICurrentUserService currentUserService, DataContext context)
            {
                _currentUserService = currentUserService;
                _context = context;
            }

            public async Task<Result<List<UserQuestResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _currentUserService.GetCurrentUser();

                if (user is null)
                {
                    return Result.Failure<List<UserQuestResponse>>(AuthErrors.AuthenticationRequiredError);
                }

                var quests = await _context
                    .UserQuests
                    .Include(uq => uq.Quest)
                    .Where(uq => uq.UserId == user.Id)
                    .Select(uq => new UserQuestResponse(
                        uq.Quest.Id,
                        uq.Quest.Title,
                        uq.Quest.Description,
                        uq.isCompleted,
                        uq.Quest.XpReward
                    ))
                    .ToListAsync();



                return Result.Success(quests);
            }
        }
    }
}
