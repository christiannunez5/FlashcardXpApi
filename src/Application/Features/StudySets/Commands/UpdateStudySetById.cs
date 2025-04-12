using Application.Common.Abstraction;
using Application.Common.Models;
using Domain.Entities.Studysets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands
{
    public static class UpdateStudySetById
    {
        public class Command : IRequest<Result>
        {
            public required string Id { get; set; }
            public required string Title { get; set; }
            public string Description { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<Command, Result>
        {

            private readonly IApplicationDbContext _context;
            private readonly IUserContext _userContext;
            public Handler(IApplicationDbContext context, IUserContext userContext)
            {
                _context = context;
                _userContext = userContext;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var studySet = await _context
                    .StudySets
                    .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);

                if (studySet == null)
                {
                    return Result.Failure(StudySetErrors.StudySetNotFound);
                }

                if (studySet.CreatedById != _userContext.UserId())
                {
                    return Result.Failure(StudySetErrors.NotOwner);
                }

                studySet.Title = request.Title;
                studySet.Description = request.Description;

                _context.StudySets.Update(studySet);

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
