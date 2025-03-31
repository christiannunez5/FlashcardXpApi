using AutoMapper;
using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Contracts;
using FlashcardXpApi.Application.Features.Auth;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.StudySets
{
    public static class GetCurrentUserRecentStudySets
    {
        public class Query : IRequest<Result<List<StudySetSummaryResponse>>>
        {

        };

        public class Handler : IRequestHandler<Query, Result<List<StudySetSummaryResponse>>>
        {

            private readonly DataContext _context;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;

            public async Task<Result<List<StudySetSummaryResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _currentUserService.GetCurrentUser();

                if (user is null)
                {
                    return Result.Failure<List<StudySetSummaryResponse>>(AuthErrors.AuthenticationRequiredError);
                }

                var studySets = await _context.StudySets
                    .Include(s => s.Flashcards)
                    .Where(s => s.CreatedById == user.Id)
                    .ToListAsync();

                return Result.Success(_mapper.Map<List<StudySetSummaryResponse>>(studySets));
            }
        }
    }
}
