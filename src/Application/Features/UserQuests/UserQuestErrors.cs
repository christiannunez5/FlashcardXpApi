

using Application.Common.Models;

namespace Application.Features.UserQuests;

public class UserQuestErrors
{
    public static Error UserQuestNotFound =
        new Error(ErrorTypeConstant.NOT_FOUND, "User quest is not found");

    public static Error UserQuestNotCompleted =
        new Error(ErrorTypeConstant.BAD_REQUEST, "The user quest must be completed before gaining experience");

    public static Error QuestAlreadyCompleted =
        new Error(ErrorTypeConstant.BAD_REQUEST, "You already completed this quest");
    
    public static Error RequirementsNotMet =
        new Error(ErrorTypeConstant.BAD_REQUEST, "You have not yet completed the quest requirements.");
}
