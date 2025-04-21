

using Application.Common.Abstraction;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserQuests.Commands;

public static class MarkQuestAsComplete
{
    public class Command : IRequest<Result>
    {
        public required string Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContext  _userContext;
        
        public Handler(IApplicationDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var userQuest = await _context
                .UserQuests
                .Include(uq => uq.Quest)
                .FirstOrDefaultAsync(uq => uq.Id == request.Id, cancellationToken);
            
            if (userQuest == null)
            {
                return Result.Failure(UserQuestErrors.UserQuestNotFound);
            }
            
            if (userQuest.IsCompleted)
            {
                return Result.Failure(UserQuestErrors.QuestAlreadyCompleted);
            }
            
            var completedFlashcardsToday = await _context
                .CompletedFlashcards
                .Where(cf => cf.UserId == _userContext.UserId() &&
                             cf.Date == DateOnly.FromDateTime(DateTime.UtcNow))
                .ToListAsync(cancellationToken);
            
            if (completedFlashcardsToday.Count < userQuest.Quest.Goal)
            {
                return Result.Failure(UserQuestErrors.QuestAlreadyCompleted);
            }
            
            userQuest.IsCompleted = true;
            
            var flashcardsToDelete = await _context
                .CompletedFlashcards
                .Take(userQuest.Quest.Goal)
                .ToListAsync(cancellationToken);
            
            _context.UserQuests.Update(userQuest);
            _context.CompletedFlashcards.RemoveRange(flashcardsToDelete);
            
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
