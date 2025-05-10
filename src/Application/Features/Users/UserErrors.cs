using Application.Common.Models;

namespace Application.Features.Users;

public class UserErrors
{
    public static Error UserNotFound =
        new Error(ErrorTypeConstant.NOT_FOUND, "User not found");   
}