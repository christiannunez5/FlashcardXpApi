using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.StudySets.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Queries;

public static class GetStudySetById
{
    public class Query : IRequest<Result<StudySetDto>>
    {
        public required string Id { get; init; }
    }
    
    public class Handler : IRequestHandler<Query,  Result<StudySetDto>>
    {
        
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public Handler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<StudySetDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var studySet = await _context
                .StudySets
                .Include(s => s.Flashcards.OrderBy(f => f.CreatedAt))
                .Include(s => s.CreatedBy)
                .FirstOrDefaultAsync(rs => rs.Id == request.Id, cancellationToken);

            if (studySet == null)
            {
                return Result.Failure<StudySetDto>(StudySetErrors.StudySetNotFound);
            }

            return Result.Success(_mapper.Map<StudySetDto>(studySet));
        }
    }
}