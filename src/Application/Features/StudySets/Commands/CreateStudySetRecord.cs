using Application.Common.Abstraction;
using Application.Common.Models;
using Domain.Entities.Studysets;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands;

public static class CreateStudySetRecord
{
    public class Command : IRequest<Result>
    {
        public required string StudySetId { get; set; }
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
                .FirstOrDefaultAsync(s => s.Id == request.StudySetId, cancellationToken);

            if (studySet == null)
            {
                return Result.Failure(StudySetErrors.StudySetNotFound);
            }
            
            var didUserAlreadyStudied = await _context
                .StudySetRecords
                .AnyAsync(sr => sr.StudiedById == _userContext.UserId() &&
                                sr.StudySetId == request.StudySetId, cancellationToken);

            if (didUserAlreadyStudied)
            {
                return Result.Failure(StudySetErrors.UserAlreadyStudied);
            }
            
            var newStudySetRecord = new StudySetRecord
            {
                StudiedById = _userContext.UserId(),
                StudySetId = studySet.Id
            };
            
            _context.StudySetRecords.Add(newStudySetRecord);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();

        }
    }
}