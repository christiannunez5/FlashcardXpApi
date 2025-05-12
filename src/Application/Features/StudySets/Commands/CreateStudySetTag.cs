using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Tags;
using Application.Features.Tags.Payloads;
using AutoMapper;
using Domain.Entities.Tags;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudySets.Commands;

public static class CreateStudySetTag
{
    public class Command : IRequest<Result<TagDto>>
    {
        public required string TagId { get; set; }
        public required  string StudySetId { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<TagDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        
        public Handler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Result<TagDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var studyset = await _context
                .StudySets
                .Include(s => s.StudySetTags)
                .FirstOrDefaultAsync(s => s.Id == request.StudySetId, cancellationToken);
            
            if (studyset == null)
            {
                return Result.Failure<TagDto>(StudySetErrors.StudySetNotFound);
            }
            
            var tag = await _context
                .Tags
                .FirstOrDefaultAsync(t => t.Id == request.TagId, cancellationToken);
            
            if (tag == null)
            {
                return Result.Failure<TagDto>(TagErrors.TagNotFound);
            }
            
            var isTagAlreadyAdded = await _context
                .StudySetTags
                .AnyAsync(st => st.StudySetId == request.StudySetId && st.TagId == request.TagId, cancellationToken);

            if (isTagAlreadyAdded)
            {
                return Result.Failure<TagDto>(StudySetErrors.TagAlreadyAdded);
            }
                        
            studyset.AddTag(tag);
            await _context.SaveChangesAsync(cancellationToken);
            
            return Result.Success(_mapper.Map<TagDto>(tag));
        }
    }
}