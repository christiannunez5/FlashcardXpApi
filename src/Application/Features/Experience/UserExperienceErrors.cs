using FlashcardXpApi.Application.Common;

namespace FlashcardXpApi.Application.Features.Experience;

public class UserExperienceErrors
{
    public static Error UserExperienceNotFound = 
        new Error(ErrorTypeConstant.NOT_FOUND, "User experience not found");
    
}