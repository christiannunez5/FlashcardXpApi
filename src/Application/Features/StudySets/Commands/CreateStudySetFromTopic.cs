using Application.Common.Abstraction;
using Application.Common.Models;
using Domain.Entities.Studysets;
using MediatR;

namespace Application.Features.StudySets.Commands;

public static class CreateStudySetFromTopic
{
    public class Command : IRequest<Result<string>>
    {
        public required string Topic { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IUserContext _userContext;
        private readonly IApplicationDbContext _context;
        private readonly IAiService _aiService;
        private readonly IDateTimeProvider _dateTimeProvider;
        
        public Handler(IUserContext userContext, IApplicationDbContext context, IAiService aiService, IDateTimeProvider dateTimeProvider)
        {
            _userContext = userContext;
            _context = context;
            _aiService = aiService;
            _dateTimeProvider = dateTimeProvider;
        }
        
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var newFlashcards = await _aiService.GenerateFlashcardFromTopic(request.Topic, cancellationToken);

            var newStudySet = new StudySet()
            {
                Flashcards = newFlashcards,
                CreatedById = _userContext.UserId(),
                Title = request.Topic,
                CreatedAt = DateOnly.FromDateTime(_dateTimeProvider.Today())
            };
            
            _context.StudySets.Add(newStudySet);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(newStudySet.Id);
        }
    }
}