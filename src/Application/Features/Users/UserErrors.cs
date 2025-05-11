using Application.Common.Models;

namespace Application.Features.Users;

public class UserErrors
{
    public static Error UserNotFound =
        new Error(ErrorTypeConstant.NOT_FOUND, "User not found");  
    
    public static Error AlreadyFollowing =
        new Error(ErrorTypeConstant.BAD_REQUEST, "You are already following this user");   
    
    public static Error CannotFollowSelf =
        new Error(ErrorTypeConstant.BAD_REQUEST, "Cannot follow yourself.");   
}