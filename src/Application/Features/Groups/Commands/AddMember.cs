using Application.Common.Abstraction;
using Application.Common.Models;
using Application.Features.Auth;
using Application.Features.Auth.Payloads;
using AutoMapper;
using Domain.Entities.Auth;
using Domain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Groups.Commands;

public static class AddMember
{
    public class Command : IRequest<Result<UserDto>>
    {
        public required string UserId { get; set; }
        public required string GroupId { get; set; }
    }
    
    public class Handler : IRequestHandler<Command, Result<UserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserContext _userContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        
        public Handler(IApplicationDbContext context, IUserContext userContext, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userContext = userContext;
            _userManager = userManager;
            _mapper = mapper;
        }


        public async Task<Result<UserDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var group = await _context
                .Groups
                .Include(g => g.GroupMembers)
                .Include(g => g.CreatedBy)
                .FirstOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);
            
           var user = await _userManager.FindByIdAsync(request.UserId);
            
           if (user == null)
           {
               // TODO: add an error class dedicated for user errors
               return Result.Failure<UserDto>(new Error(ErrorTypeConstant.NOT_FOUND, "User not found"));
           }
           
           if (group == null)
           {
               return Result.Failure<UserDto>(GroupErrors.GroupNotFound);
           }
           
           group.AddMember(user.Id);
           
           await _context.SaveChangesAsync(cancellationToken);
            
           return Result.Success(_mapper.Map<UserDto>(user));
        }
    }
}