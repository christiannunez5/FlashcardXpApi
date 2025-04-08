using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Common.Interfaces;
using FlashcardXpApi.Application.Contracts;
using FlashcardXpApi.Application.Contracts.Flashcards;
using FlashcardXpApi.Application.Features.Auth;
using FlashcardXpApi.Domain;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.StudySets
{
    public static class UpdateStudySet
    {
        public class Command : IRequest<Result>
        {
            public required string Id { get; set; }
            public required string Title { get; set; }
            public required string Description { get; set; }
            
        };
        
        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly DataContext _context;
            private readonly ICurrentUserService _currentUserService;
    
            public Handler(DataContext context, ICurrentUserService currentUserService)
            {
                _context = context;
                _currentUserService = currentUserService;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _currentUserService.GetCurrentUser();

                if (user is null)
                {
                    return Result.Failure(AuthErrors.AuthenticationRequiredError);
                }
                
                var studySet = await _context
                    .StudySets
                    .FirstOrDefaultAsync(s => s.Id == request.Id);
                
                if (studySet == null)
                {
                    return Result.Failure(StudySetErrors.StudySetNotFoundError);
                }
                    
                if (studySet.CreatedById != user.Id)
                {
                    return Result.Failure(StudySetErrors.NotStudySetOwner);
                }
                
                studySet.Title = request.Title;
                studySet.Description = request.Description;
                studySet.Status = StudySetStatus.Published;
                studySet.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
                
                await _context.SaveChangesAsync(cancellationToken);
                return Result.Success();
                
            }
        }

    }
}
