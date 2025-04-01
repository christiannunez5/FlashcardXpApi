using AutoMapper;
using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Contracts;
using FlashcardXpApi.Application.Features.Auth;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.RecentStudySets
{
    public static class GetCurrentUserRecentStudySets
    {
        public class Query : IRequest<Result<List<RecentStudySetResponse>>>
        {

        };

        public class Handler : IRequestHandler<Query, Result<List<RecentStudySetResponse>>>
        {

            private readonly DataContext _context;
            private readonly ICurrentUserService _currentUserService;
            private readonly IMapper _mapper;

            public Handler(DataContext context, ICurrentUserService currentUserService, IMapper mapper)
            {
                _context = context;
                _currentUserService = currentUserService;
                _mapper = mapper;
            }

            public async Task<Result<List<RecentStudySetResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await _currentUserService.GetCurrentUser();

                if (user is null)
                {
                    return Result.Failure<List<RecentStudySetResponse>>(AuthErrors.AuthenticationRequiredError);
                }

                var studySets = await _context.RecentStudySets
                    .Where(rs => rs.UserId == user.Id)
                    .Include(rs => rs.StudySet)
                    .OrderByDescending(rs => rs.AccessedAt)
                    .Select(rs => new RecentStudySetResponse(
                        rs.StudySetId,
                        rs.StudySet.Title,
                        rs.AccessedAt
                    ))
                    .ToListAsync();

                return Result.Success(studySets);
            }
        }
    }
}
