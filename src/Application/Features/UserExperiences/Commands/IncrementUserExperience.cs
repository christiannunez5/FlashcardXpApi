
using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.UserExperiences.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserExperiences.Commands;

public static class IncrementUserExperience
{
    public class Command : IRequest<Result<UserExperienceDto>>
    {
        public required int Xp { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<UserExperienceDto>>
    {

        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;

        public Handler(IMapper mapper, IApplicationDbContext context, IUserContext userContext)
        {
            _mapper = mapper;
            _context = context;
            _userContext = userContext;
        }

        public async Task<Result<UserExperienceDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userExperience = await _context
               .UserExperiences
               .FirstOrDefaultAsync(r => r.UserId == _userContext.UserId(), cancellationToken);

            if (userExperience == null)
            {
                throw new ApplicationException("User does not have user experience implemented");
            }

            userExperience.Xp += request.Xp;
            _context.UserExperiences.Update(userExperience);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(_mapper.Map<UserExperienceDto>(userExperience));
        }
    }
}
