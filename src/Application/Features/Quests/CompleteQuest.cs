using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.Quests
{
    public static class CompleteQuest
    {
        public class Command : IRequest<Result<string>>
        {
            public required string Id { get; set; }
        };

        public class Handler : IRequestHandler<Command, Result<string>>
        {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {

                var quest = await _context
                    .UserQuests
                    .FirstOrDefaultAsync(uq => uq.QuestId == request.Id);

                if (quest is null)
                {
                    return Result.Failure<string>(QuestErrors.QuestNotFound);
                }

                quest.isCompleted = true;

                _context.UserQuests.Update(quest);

                await _context.SaveChangesAsync(cancellationToken);
                
                return Result.Success(quest.QuestId);
            }
        }
    }
}
