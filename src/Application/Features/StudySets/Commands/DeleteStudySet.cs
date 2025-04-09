using Application.Common.Abstraction;
using Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands;

public static class DeleteStudySet
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
                .FirstOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            
            if (studySet == null)
            {
                return Result.Failure<string>(StudySetErrors.StudySetNotFound);
            }
            
            _context.StudySets.Remove(studySet);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(request.Id);

        }
    }
}