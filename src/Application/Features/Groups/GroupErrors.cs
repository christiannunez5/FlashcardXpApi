using Application.Common.Models;

namespace Application.Features.Groups;

public class GroupErrors
{
    public static Error GroupNotFound =
        new Error(ErrorTypeConstant.NOT_FOUND, "Group not found");
    
    public static Error UserAlreadyParticipant =
        new Error(ErrorTypeConstant.BAD_REQUEST, "Group not found");
    
    public static Error UserIsNotPartOfTheGroup =
        new Error(ErrorTypeConstant.BAD_REQUEST, "Group not found");
}