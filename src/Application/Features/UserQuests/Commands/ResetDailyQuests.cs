using Application.Common.Abstraction;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserQuests.Commands;

public static class ResetDailyQuests
{
    public class Command : IRequest<Result>
    {

    }
    
    public class Handler : IRequestHandler<Command, Result>
    {

        private readonly IUserContext _userContext;
        private readonly IApplicationDbContext _context;

        public Handler(IUserContext userContext, IApplicationDbContext context)
        {
            _userContext = userContext;
            _context = context;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var userQuests = await _context
                .UserQuests
                .Where(uq => uq.UserId == _userContext.UserId())
                .ToListAsync(cancellationToken);

            foreach (var quest in userQuests)
            {
                quest.IsCompleted = false;
                _context.UserQuests.Update(quest);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}