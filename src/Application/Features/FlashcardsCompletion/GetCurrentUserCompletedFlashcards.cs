using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;

namespace FlashcardXpApi.Application.Features.FlashcardsCompletion
{
    public static class GetCurrentUserCompletedFlashcards
    {
        public class Command : IRequest<Result<int>>
        {

        };

        public class Handler : IRequestHandler<Command, Result<int>>
        {

            private readonly DataContext _context;



            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                // var completedFlashcardsToday = await _context
            }
        }
    }
}
