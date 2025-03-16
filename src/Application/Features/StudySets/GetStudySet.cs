using AutoMapper;
using FlashcardXpApi.Application.Common;
using FlashcardXpApi.Application.Contracts;
using FlashcardXpApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlashcardXpApi.Application.Features.StudySets
{
    public static class GetStudySet
    {
        public class Query : IRequest<Result<StudySetResponse>>
        {
            public required string Id { get; set; }
        };

        public class Handler : IRequestHandler<Query, Result<StudySetResponse>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<StudySetResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var studySet = await _context
                    .StudySets
                    .Include(s => s.Flashcards)
                    .Include(s => s.CreatedBy)
                    .AsSingleQuery()
                    .FirstOrDefaultAsync(s => s.Id == request.Id);

                if (studySet is null)
                {
                    return Result.Failure<StudySetResponse>(StudySetErrors.StudySetNotFoundError);
                }

                return Result.Success(_mapper.Map<StudySetResponse>(studySet)); 
            }
        }
    }
}
