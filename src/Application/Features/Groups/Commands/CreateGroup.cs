using Application.Common.Abstraction;
using Application.Common.Models;
using Domain.Entities.Groups;
using MediatR;

namespace Application.Features.Groups.Commands;

public static class CreateGroup
{
    public class Command : IRequest<Result<string>>
    {
        public required string Name { get; set; }
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
            var newClass = new Group
            {
                Name = request.Name,
                CreatedById = _userContext.UserId()
            };
            
            _context.Groups.Add(newClass);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(newClass.Id);
        }
    }
}