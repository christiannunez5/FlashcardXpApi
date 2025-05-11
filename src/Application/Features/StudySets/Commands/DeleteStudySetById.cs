using Application.Common.Abstraction;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands;

public static class DeleteStudySetById
{
    public class Command : IRequest<Result<string>>
    {
        public required string Id { get; init; }
    }
    
    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;
        private readonly IEventService _eventService;
        
        public Handler(IApplicationDbContext context, IUserContext userContext, IEventService eventService)
        {
            _context = context;
            _userContext = userContext;
            _eventService = eventService;
        }
        
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            
            var studySet = await _context
                .StudySets
                .Include(s => s.Flashcards)
                .Include(s => s.StudySetParticipants)
                .Include(s => s.RecentStudySets)
                .Include(s => s.GroupStudySets)
                .Include(s => s.StudySetRatings)
                .Include(s => s.StudySetRecords)
                .AsSingleQuery()
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            
            if (studySet == null)
            {
                return Result.Failure<string>(StudySetErrors.StudySetNotFound);
            }

            if (studySet.CreatedById != _userContext.UserId())
            {
                return Result.Failure<string>(StudySetErrors.NotOwner);
            }
            
            // remove all children to studysets
            _context.GroupStudySets.RemoveRange(studySet.GroupStudySets);
            _context.RecentStudySets.RemoveRange(studySet.RecentStudySets);
            _context.StudySetParticipants.RemoveRange(studySet.StudySetParticipants);
            _context.Flashcards.RemoveRange(studySet.Flashcards);
            _context.StudySetRatings.RemoveRange(studySet.StudySetRatings);
            _context.StudySetRecords.RemoveRange(studySet.StudySetRecords);
            _context.StudySets.Remove(studySet);
            
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success(request.Id);

        }
    }
}