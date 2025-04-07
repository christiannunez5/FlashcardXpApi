using AutoMapper;
using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Contracts;
using FlashcardXpApi.Application.Contracts.StudySets;
using FlashcardXpApi.Application.Features.Auth;
using FlashcardXpApi.Application.Features.StudySets;
using FlashcardXpApi.Domain;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.RecentStudySets
{
    public static class CreateRecentStudySet
    {
        public class Command : IRequest<Result<RecentStudySetResponse>>
        {
            public required string Id { get; set; }
        };

        public class Handler : IRequestHandler<Command, Result<RecentStudySetResponse>>
        {

            private readonly DataContext _context;
            private readonly ICurrentUserService _currentUserService;

            public Handler(DataContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<Result<RecentStudySetResponse>> Handle(Command request, CancellationToken cancellationToken)
            {

                var user = await _currentUserService.GetCurrentUser()!;

                if (user is null)
                {
                    return Result.Failure<RecentStudySetResponse>(AuthErrors.AuthenticationRequiredError);
                }

                var studySet = await _context
                    .StudySets
                    .FirstOrDefaultAsync(s => s.Id == request.Id);

                if (studySet == null)
                {
                    return
                        Result.Failure<RecentStudySetResponse>(StudySetErrors
                        .StudySetNotFoundError);
                }

                var recentStudySet = await _context
                    .RecentStudySets
                    .FirstOrDefaultAsync(rs => rs.UserId == user.Id &&
                    rs.StudySetId == request.Id);

                if (recentStudySet is not null)
                {
                    recentStudySet.AccessedAt = DateTime.Now;
                    _context.RecentStudySets.Update(recentStudySet);
                }

                else
                {
                    recentStudySet = new RecentStudySet
                    {
                        UserId = user.Id,
                        StudySetId = studySet.Id,
                    };

                    _context.RecentStudySets.Add(recentStudySet);
                }

                await _context.SaveChangesAsync();

                return Result.Success(new RecentStudySetResponse(
                    studySet.Id,
                    studySet.Title,
                    recentStudySet.AccessedAt
                ));

            }
        }
    }
}
