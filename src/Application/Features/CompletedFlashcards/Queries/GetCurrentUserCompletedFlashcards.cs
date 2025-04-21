

using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.CompletedFlashcards.Payloads;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CompletedFlashcards.Queries;

public static class GetCurrentUserCompletedFlashcards
{
    public class Query : IRequest<Result<CompletedFlashcardDto>>
    {

    }

    public class Handler : IRequestHandler<Query, Result<CompletedFlashcardDto>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;

        public Handler(IApplicationDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<Result<CompletedFlashcardDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var completedFlashcardsTodayCount = await _context
                .CompletedFlashcards
                .Where(cf => cf.UserId == _userContext.UserId() &&
                       cf.Date == DateOnly.FromDateTime(DateTime.UtcNow))
                .CountAsync(cancellationToken);
            
            var completedFlashcardDto = new CompletedFlashcardDto(completedFlashcardsTodayCount);
            
            return Result.Success(completedFlashcardDto);
        }
    }
}
