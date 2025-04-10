
using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.StudySets.Payloads;
using AutoMapper;
using Domain.Entities.Studysets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands;

public static class CreateRecentStudySet
{
    public class Command : IRequest<Result<RecentStudySetDto>>
    {
        public required string StudySetId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<RecentStudySetDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;

        public Handler(IApplicationDbContext context, IMapper mapper, IUserContext userContext)
        {
            _context = context;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<Result<RecentStudySetDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var doesStudySetExist = await _context
                .StudySets
                .AnyAsync(s => s.Id == request.StudySetId, cancellationToken);

            if (!doesStudySetExist)
            {
                return Result.Failure<RecentStudySetDto>(StudySetErrors.StudySetNotFound);
            }

            var newRecentStudySet = new RecentStudySet
            {
                StudySetId = request.StudySetId,
                UserId = _userContext.UserId()
            };

            _context.RecentStudySets.Add(newRecentStudySet);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(_mapper.Map<RecentStudySetDto>(newRecentStudySet));
        }
    }
}
