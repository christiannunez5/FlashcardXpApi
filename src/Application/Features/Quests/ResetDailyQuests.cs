using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Features.Auth;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.Quests
{
    public static class ResetDailyQuests
    {

        public class Command : IRequest<Result>
        {

        };
        
        public class Handler : IRequestHandler<Command, Result>
        {

            private readonly DataContext _context;
            private readonly ICurrentUserService _currentUserService;

            public Handler(DataContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _currentUserService.GetCurrentUser();

                if (user is null)
                {
                    return Result.Failure(AuthErrors.AuthenticationRequiredError);
                }

                var quests = await _context
                    .UserQuests
                    .ToListAsync();
                
                DateOnly currentDate = DateOnly.FromDateTime(DateTime.UtcNow);

                foreach (var quest in quests)
                {
                    if (quest.CurrentQuestDate < currentDate)
                    {
                        quest.isCompleted = false;
                        _context.UserQuests.Update(quest);
                    }
                }

                await _context.SaveChangesAsync();

                return Result.Success();

            }
        }

    }
}
