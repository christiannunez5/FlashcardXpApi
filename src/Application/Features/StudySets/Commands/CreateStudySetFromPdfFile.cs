using Application.Common.Abstraction;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities.Studysets;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands;

public static class CreateStudySetFromPdfFile
{
    public class Command : IRequest<Result<string>>
    {
        public string? StudySetId { get; set; }
        public required IFormFile File { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<string>>
    {
        private readonly IFileToTextService _fileToText;
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;
        private readonly IAiService _aiService;
        private readonly IDateTimeProvider _dateTimeProvider;
        
        public Handler(IApplicationDbContext context, IUserContext userContext, 
            IFileToTextService fileToText, IAiService aiService, IDateTimeProvider dateTimeProvider)
        {
            _context = context;
            _userContext = userContext;
            _fileToText = fileToText;
            _aiService = aiService;
            _dateTimeProvider = dateTimeProvider;
        }
        
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            if (request.File.ContentType != "application/pdf")
            {
                return Result.Failure<string>(StudySetErrors.InvalidFileTypePdf);
            }
            
            var stream = request.File.OpenReadStream();
            string textFromFile = _fileToText.ExtractTextFromFile(stream);

            var studySet = await _context
                .StudySets
                .FirstOrDefaultAsync(s => s.Id == request.StudySetId, cancellationToken);
            
            var newFlashcards = await _aiService.GenerateFlashcardFromText(textFromFile, cancellationToken);

            if (studySet is null)
            {
                studySet = new StudySet
                {
                    Title = request.File.FileName.Split('.').First(),
                    CreatedById = _userContext.UserId(),
                    Flashcards = newFlashcards,
                    CreatedAt = DateOnly.FromDateTime(_dateTimeProvider.Today())
                };
                _context.StudySets.Add(studySet);
            }
            else
            {
                foreach (var newFlashcard in newFlashcards)
                {
                    studySet.Flashcards.Add(newFlashcard);
                }
            }
            
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(studySet.Id);
        }
    }
}