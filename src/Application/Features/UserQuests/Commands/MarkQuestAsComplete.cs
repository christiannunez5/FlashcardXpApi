

using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Quests.Payloads;
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

        public Handler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var userQuest = await _context
                .UserQuests
                .FirstOrDefaultAsync(uq => uq.Id == request.Id);

            if (userQuest == null)
            {
                return Result.Failure(UserQuestErrors.UserQuestNotFound);
            }

            if (userQuest.IsCompleted)
            {
                return Result.Failure(UserQuestErrors.QuestAlreadyCompleted);
            }

            userQuest.IsCompleted = true;
            _context.UserQuests.Update(userQuest);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
