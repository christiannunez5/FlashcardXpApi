

using Application.Common.Abstraction;
using Application.Common.Models;
using Domain.Entities.Studysets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands;

public static class UpdateStudySetStatus
{
    public class Command : IRequest<Result<string>>
    {
        public required string Id { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<string>>
    {

        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;

        public Handler(IApplicationDbContext context, IUserContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var studySet = await _context
                .StudySets
                .FirstOrDefaultAsync(s => s.Id == request.Id);

            if (studySet == null)
            {
                return Result.Failure<string>(StudySetErrors.StudySetNotFound);
            }

            if (studySet.CreatedById != _userContext.UserId())
            {
                return Result.Failure<string>(StudySetErrors.NotOwner);
            }

            studySet.Status = StudySetStatus.Published;

            _context.StudySets.Update(studySet);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(studySet.Id);
        }
    }
}
