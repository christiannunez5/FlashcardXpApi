using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Auth.Payloads;
using Application.Features.UserExperiences.Payloads;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserExperiences.Query;

public static class GetCurrentUserExperience
{
    public class Query : IRequest<Result<UserExperienceDto>>
    {

    }

    public class Handler : IRequestHandler<Query, Result<UserExperienceDto>>
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

        public async Task<Result<UserExperienceDto>> Handle(Query request, CancellationToken cancellationToken)
        {

            var userExperience = await _context
                .UserExperiences
                .Include(uxp => uxp.User)
                .FirstOrDefaultAsync(uxp => uxp.UserId == _userContext.UserId(), cancellationToken);

            if (userExperience == null)
            {
                throw new ApplicationException("User does not have user experience implemented");
            }

            return Result.Success(_mapper.Map<UserExperienceDto>(userExperience));
        }
    }
}
